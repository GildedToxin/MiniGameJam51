using UnityEngine;

public class OxygenTank : MonoBehaviour, IPlayerLookTarget
{
    bool isLookedAt = false;
    bool isInteracting = false;
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
        GameManager.Instance.player.RestoreOxygen();
        Destroy(gameObject);
    }
    public void StopInteract()
    {
        //isInteracting = false;
    }
}
