using UnityEngine;

public class Valve : MonoBehaviour, IPlayerLookTarget
{
    public bool isLookedAt; // Whether the player is currently looking at the valve
    public bool isInteracting; // Whether the player is currently looking at the valve
    public float turnPrecentage;
    public float turnSpeed;

    public float looseSpeed;
  public void OnLookEnter()
  {
        isLookedAt = true;
  }
  public void OnLookExit()
  {
        isLookedAt = false;
  }
    public void Interact()
    {
        isInteracting = true;
    }
    public void StopInteract()
    {
        isInteracting = false;
    }

    public void Update()
    {
        if (isInteracting)
        {
            turnPrecentage += turnSpeed * Time.deltaTime;
        }
        else if (turnPrecentage > 0 && turnPrecentage < 10)
        {
            turnPrecentage -= looseSpeed * Time.deltaTime;
        }

        turnPrecentage = Mathf.Clamp(turnPrecentage, 0, 10);
    }
}
