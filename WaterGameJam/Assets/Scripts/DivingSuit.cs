using UnityEngine;

public class DivingSuit : MonoBehaviour, IPlayerLookTarget
{
    bool isLookedAt = false;
    bool isInteracting = false;

    public DialogueGroup dialogueGroup;

    public GameObject spawnRoom;
    public Material blackMat;
    public AudioSource audioSource;
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
        try
        {
            audioSource.Play();
        }
        catch
        {
            Debug.Log("Could not play audio source");
        }
        
        Destroy(gameObject);

        foreach (Renderer r in spawnRoom.GetComponentsInChildren<Renderer>())
        {
            r.material = blackMat;
        }
        GameManager.Instance.hasSuit = true;
        GameManager.Instance.playerCanPing = true;

    }
    public void StopInteract()
    {
        //isInteracting = false;
    }
}
