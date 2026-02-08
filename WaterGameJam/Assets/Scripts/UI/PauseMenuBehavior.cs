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
        float shownVolume = value * 100;
        volumeText.SetText($"{shownVolume.ToString("n0")}");
        //mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
        mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
        if (value == 0)
        {
            mixer.SetFloat("Volume", -80);
        }

    }
    public void UnPause()
    {
        ResumeGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.LoadScene("SampleScene"); //replace with home menu scene name
    }
}
