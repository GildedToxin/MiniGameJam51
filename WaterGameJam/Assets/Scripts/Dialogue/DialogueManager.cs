using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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

    public AudioClip pingtime;
    public AudioClip canPing;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
           // DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        try
        {
            dialogueLineRunner.gameObject.SetActive(false);
        }
        catch
        {
            Debug.Log("Could not find Dialogue Line Runner");
        }
    }

    public void PlayDialogueSequence(DialogueGroup dialogueGroup)
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;
        }
        //AudioSource audioSource = speakers[0].GetComponent<AudioSource>(); ;
        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        this.dialogueGroup = dialogueGroup;
        try
        {
            dialogueRoutine = StartCoroutine(PlaySequence());
        }
        catch
        {
            Debug.Log("Could not find dialogueRoutine");
        }
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

            if (clip == pingtime)
            {
                FindAnyObjectByType<SonarScript>().FollowPlayer();
                FindAnyObjectByType<SonarScript>().SonarPing();
            }
            if (clip == canPing)
            {
                FindAnyObjectByType<SonarScript>().dialogueWait = false;
            }


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
            FindAnyObjectByType<PlayerController>().canMove = true;
        }
        dialogueLineRunner.gameObject.SetActive(false);
        Debug.Log("Dialogue sequence finished");
    }
}