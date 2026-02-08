using UnityEngine;

public class OxygenTank : MonoBehaviour, IPlayerLookTarget
{
    bool isLookedAt = false;
    bool isInteracting = false;

    DialogueGroup dialogueGroup;
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
        Destroy(gameObject);



    }
    public void StopInteract()
    {
        //isInteracting = false;
    }
}
