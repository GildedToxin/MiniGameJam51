using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    private Vector3 movement;
    [SerializeField] private int moveSpeed = 10;
    public bool canMove = true;
    public Rigidbody rb;

    [Header("Jump")]
    public bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 6f;

    [Header("Look")]
    private Vector2 look;
    public Camera playerCamera;
    public float mouseSensitivity = 1f;
    private float xRotation = 0f;

    private IPlayerLookTarget currentLookAt;
    [SerializeField] private float interactionDistance = 5f;

    [Header("Oxygen")]
    public float oxygenLevel = 100f;
    private float oxygenDepletionRate = 5f;

    [Header("Slope Handling")]
    [SerializeField] private float groundCheckDistance = 1.3f;
    private RaycastHit groundHit;


    [SerializeField] private float airControl = 0.4f;
    [SerializeField] private float extraGravity = 25f;
    [SerializeField] private float maxFallSpeed = -20f;


    private void Start()
    {
        GameManager.Instance.player = this;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        DepleteOxygen();
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        if (!canMove) return;

        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveInput = (camForward * movement.z + camRight * movement.x);

        if (isGrounded)
        {
            // Stick to slope
            moveInput = Vector3.ProjectOnPlane(moveInput, groundHit.normal);

            rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);

            // Ground stick
            rb.AddForce(Vector3.down * 30f, ForceMode.Acceleration);
        }
        else
        {
            // Air movement (velocity-based)
            Vector3 airMove = moveInput.normalized * moveSpeed * airControl;
            rb.linearVelocity = new Vector3(
                airMove.x,
                rb.linearVelocity.y,
                airMove.z
            );

            // Strong gravity = no float
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }

        // Clamp fall speed
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxFallSpeed, rb.linearVelocity.z);
        }
    }

    private void LateUpdate()
    {
        float mouseX = look.x * mouseSensitivity;
        float mouseY = look.y * mouseSensitivity;

        // Rotate camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);
        LookDirection();
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
    }
    public void OnLook(InputAction.CallbackContext ctx)
    {
        look = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;
        if (!isGrounded) return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }
    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            out groundHit,
            groundCheckDistance,
            groundLayer
        );
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            currentLookAt?.Interact();

        if (ctx.canceled)
            currentLookAt?.StopInteract();
    }

    public void OnSonar(InputAction.CallbackContext ctx)
    {
        print("sonar pinged");  
        if (ctx.started)
            GameManager.Instance.SonarPing(this.transform.position);    
    }

    public void LookDirection() 
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            IPlayerLookTarget lookable = hit.collider.GetComponent<IPlayerLookTarget>()
                          ?? hit.collider.GetComponentInParent<IPlayerLookTarget>();
            if (lookable != null)
            {
                // If looking at a new object, turn off the last one and turn on the new one
                if (currentLookAt != lookable)
                {
                    currentLookAt?.OnLookExit();
                    currentLookAt = lookable;
                    currentLookAt.OnLookEnter();
                }
                return;
            }
        }

        // If nothing is hit, turn off the last object looked at
        if (currentLookAt != null)
        {
            currentLookAt.OnLookExit();
            currentLookAt = null;
        }
    }

    private void DepleteOxygen()
    {
        oxygenLevel -= oxygenDepletionRate * Time.deltaTime;
        oxygenLevel = Mathf.Clamp(oxygenLevel, 0f, 100f);

        if (oxygenLevel <= 0f)
        {
            Debug.Log("Player has run out of oxygen!");
        }
    }

    public void RestoreOxygen(float oxyRegen = 20f)
    {
        oxygenLevel += oxyRegen;
        oxygenLevel = Mathf.Clamp(oxygenLevel, 0f, 100f);
    }
}
