using UnityEngine;

public class DivingSuit : MonoBehaviour, IPlayerLookTarget
{
    bool isLookedAt = false;
    bool isInteracting = false;

    public DialogueGroup dialogueGroup;

    public GameObject spawnRoom;
    public Material blackMat;
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

        DialogueManager.Instance.PlayDialogueSequence(dialogueGroup);
        Destroy(gameObject);

        foreach (Renderer r in spawnRoom.GetComponentsInChildren<Renderer>())
        {
            r.material = blackMat;
        }

    }
    public void StopInteract()
    {
        //isInteracting = false;
    }
}
