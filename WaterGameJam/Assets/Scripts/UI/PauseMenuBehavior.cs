using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuBehavior : Pause
{
    public Button Continue;
    public Button Exit;

    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private TextMeshProUGUI volumeText;

    public void onChangeVolume(float value)
    {
        volumeText.SetText($"{value.ToString()}");
        mixer.SetFloat("Master", Mathf.Log10(value) * 20);

    }
    public void UnPause()
    {
        ResumeGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.LoadScene("SampleScene"); //replace with home menu scene name
    }
