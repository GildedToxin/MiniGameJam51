using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] public List<GameObject> speakers = new List<GameObject>();

    public bool isDialogueActive = false;
    public DialogueGroup dialogueGroup;
    public int currentLineIndex = 0;
    private Coroutine dialogueRoutine;

    public DialogueLineRunner dialogueLineRunner;

    public DialogueGroup doorDialougeGroup;

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
    private void Start()
    {
        dialogueLineRunner.gameObject.SetActive(false);
    }

    public void PlayDialogueSequence(DialogueGroup dialogueGroup)
    {
        //AudioSource audioSource = speakers[0].GetComponent<AudioSource>(); ;
        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        this.dialogueGroup = dialogueGroup;
        dialogueRoutine = StartCoroutine(PlaySequence());
    }

    public void AddAudioSource(GameObject speaker, Speakers script)
    {
        speakers.Add(speaker);
    }

    private IEnumerator PlaySequence()
    {
        currentLineIndex = 0;
        foreach (AudioClip clip in dialogueGroup.audioClips)
        {
            foreach(GameObject speaker in speakers)
            {
                speaker.GetComponent<AudioSource>().clip = clip;
                speaker.GetComponent<AudioSource>().Play();
            }
            //audioSource.clip = clip;
            //audioSource.Play();

            dialogueLineRunner.gameObject.SetActive(true);
            dialogueLineRunner.text.text = dialogueGroup.lines[currentLineIndex];

            currentLineIndex++;

            // Wait until this clip finishes
            yield return new WaitWhile(() => {

                foreach(GameObject speaker in speakers)
                {
                    if (speaker.GetComponent<AudioSource>().isPlaying)
                        return true;
                }

                return false;
                });
        }
        if(dialogueGroup == doorDialougeGroup)
        {
            GameManager.Instance.door.GetComponent<MoveUp>().enabled = true;
        }
        dialogueLineRunner.gameObject.SetActive(false);
        Debug.Log("Dialogue sequence finished");
    }
}