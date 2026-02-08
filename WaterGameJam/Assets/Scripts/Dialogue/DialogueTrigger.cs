using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
  public DialogueGroup dialogueGroup;



    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerController>())
        {
            DialogueManager.Instance.PlayDialogueSequence(dialogueGroup);
            Destroy(this);
        }
    }
}
