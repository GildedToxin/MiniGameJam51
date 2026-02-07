using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Scriptable Objects/DialogueLine")]
public class DialogueGroup : ScriptableObject
{
    public string[] lines;
    public AudioClip[] audioClips;
}
