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


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
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
        if (value.isPressed)
            Debug.Log("Jump");
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
            Debug.Log("Interact");
    }
}
