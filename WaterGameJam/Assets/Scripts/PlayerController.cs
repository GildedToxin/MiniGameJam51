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

    [Header("Oxygen")]
    public float oxygenLevel = 100f;
    private float oxygenDepletionRate = 5f;
    

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

        if (!canMove ) return;

        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Relative movement based on camera orientation
        Vector3 move = (camForward * movement.z + camRight * movement.x).normalized;
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
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
    }


    public void OnMove(InputValue value)
    {
        movement = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }
    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        if (!isGrounded) return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }
    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            1.1f,
            groundLayer
        );
    }
    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
            Debug.Log("Interact");
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
