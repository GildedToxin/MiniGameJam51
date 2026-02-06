using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


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


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        if (!canMove ) return;
        // Updates potion based on input
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(InputValue value)
    {
        movement = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        if (!isGrounded) return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }


    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
            Debug.Log("Interact");
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
}
