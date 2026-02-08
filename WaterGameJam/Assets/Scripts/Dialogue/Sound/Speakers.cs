using UnityEngine;

public class Speakers : MonoBehaviour
{
    public AudioSource speaker;
    public DialogueManager dm;
    public GameObject manager;

    private void Awake()
    {
        dm = FindAnyObjectByType<DialogueManager>();
        speaker = FindAnyObjectByType<AudioSource>();
    }
    private void Start()
    {
        Debug.Log(dm);
        Debug.Log(speaker);
        dm.AddAudioSource(speaker);
    }    

}
