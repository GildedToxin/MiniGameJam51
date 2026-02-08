using UnityEngine;
using UnityEngine.UI;

public class Valve : MonoBehaviour, IPlayerLookTarget
{
    public bool isLookedAt;
    public bool isInteracting;
    public bool isTurned;
    public float turnPrecentage;
    public float turnSpeed;
    public float looseSpeed;
    public float valveIndex;
    public GameObject valveHandle;

    // Sound fields
    public AudioSource audioSource;
    public AudioClip turnSound;
    public float fadeOutTime = 0.1f;
    private bool isSoundPlaying;
    private bool isFadingOut;
    private float targetVolume;
    private float originalVolume;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        audioSource.loop = false;
        audioSource.playOnAwake = false;
        originalVolume = audioSource.volume;
    }

    public void OnLookEnter()
    {
        isLookedAt = true;
        if (!isTurned)
        {
            GameManager.Instance.hud.turn.SetActive(true);
            GameManager.Instance.hud.slider.GetComponent<Slider>().value = 0;
        }
    }

    public void OnLookExit()
    {
        isLookedAt = false;
        isInteracting = false;
        if (!isTurned)
        {
            GameManager.Instance.hud.turn.SetActive(false);
            GameManager.Instance.hud.slider.GetComponent<Slider>().value = 0;
        }
        StopTurnSound();
    }

    public void Interact()
    {
        isInteracting = true;
    }

    public void StopInteract()
    {
        isInteracting = false;
        StopTurnSound();
    }

    public void Update()
    {
        if (isTurned) return;

        if (isFadingOut)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, (originalVolume / fadeOutTime) * Time.deltaTime);
            if (audioSource.volume <= 0.01f)
            {
                audioSource.Stop();
                audioSource.volume = originalVolume;
                isFadingOut = false;
                isSoundPlaying = false;
            }
        }

        if (isInteracting && isLookedAt)
        {
            PlayTurnSound();

            float degreesPerSecond = 360f / 10;
            float delta = degreesPerSecond * Time.deltaTime;
            valveHandle.transform.Rotate(Vector3.forward, delta, Space.Self);
            turnPrecentage += turnSpeed * Time.deltaTime;
            GameManager.Instance.hud.slider.GetComponent<Slider>().value = turnPrecentage;
        }
        else
        {
            StopTurnSound();

            if (turnPrecentage > 0 && turnPrecentage < 10)
            {
                turnPrecentage = 0;
                GameManager.Instance.hud.slider.GetComponent<Slider>().value = turnPrecentage;
            }
        }

        if (turnPrecentage >= 10)
        {
            isTurned = true;
            GameManager.Instance.hud.turn.SetActive(false);
            GameManager.Instance.IncreaseWaterLevel();
            GameManager.Instance.lastTurnedValve = this;
            StopTurnSound();
        }

        turnPrecentage = Mathf.Clamp(turnPrecentage, 0, 10);
    }

    private void PlayTurnSound()
    {
        if (!isSoundPlaying && turnSound != null)
        {
            isFadingOut = false;
            audioSource.volume = originalVolume;
            audioSource.clip = turnSound;
            audioSource.Play();
            isSoundPlaying = true;
        }
    }

    private void StopTurnSound()
    {
        if (isSoundPlaying && !isFadingOut)
        {
            isFadingOut = true;
        }
    }
}