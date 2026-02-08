using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] List<AudioSource> speakers = new List<AudioSource>();

    public bool isDialogueActive = false;
    public DialogueGroup dialogueGroup;
    public int currentLineIndex = 0;
    private Coroutine dialogueRoutine;

    public DialogueLineRunner dialogueLineRunner;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayDialogueSequence(DialogueGroup dialogueGroup)
    {
        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        this.dialogueGroup = dialogueGroup;
        dialogueRoutine = StartCoroutine(PlaySequence());
    }

    public void AddAudioSource(AudioSource speaker)
    { 
        speakers.Add(speaker);
        Debug.Log("added");
    }

    private IEnumerator PlaySequence()
    {
        currentLineIndex = 0;
        foreach (AudioClip clip in dialogueGroup.audioClips)
        {
            audioSource.clip = clip;
            audioSource.Play();

            dialogueLineRunner.text.text = dialogueGroup.lines[currentLineIndex];

            currentLineIndex++;

            // Wait until this clip finishes
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        Debug.Log("Dialogue sequence finished");
    }
}
