using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        Debug.Log(move);
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
