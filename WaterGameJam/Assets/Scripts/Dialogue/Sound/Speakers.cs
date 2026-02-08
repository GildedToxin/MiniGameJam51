using UnityEngine;

public class Speakers : MonoBehaviour
{
    public AudioSource soundSource;
    public DialogueManager dm;
    public GameObject manager;
    public GameObject Speaker;

    private void Awake()
    {
        dm = FindAnyObjectByType<DialogueManager>();
        soundSource = GetComponent<AudioSource>();
        Speaker = this.gameObject;
    }
    private void Start()
    {
        dm.AddAudioSource(Speaker, this);
    }   

}
