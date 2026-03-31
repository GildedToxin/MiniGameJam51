using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeText : MonoBehaviour
{
    public float delay = 6f;
    public float fadeDuration = 2f;
    public string sceneToLoad;

    private TextMeshProUGUI tmpText;
    private Text uiText;

    void Awake()
    {
        tmpText = GetComponentInChildren<TextMeshProUGUI>();
        uiText = GetComponent<Text>();
    }

    void OnEnable()
    {
        SetAlpha(1f);
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // Wait before starting fade
        yield return new WaitForSeconds(delay);

        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            SetAlpha(alpha);

            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(0f);

 
            SceneManager.LoadScene("MainMenu");
        
    }

    void SetAlpha(float alpha)
    {
        if (tmpText != null)
        {
            Color c = tmpText.color;
            c.a = alpha;
            tmpText.color = c;
        }

        if (uiText != null)
        {
            Color c = uiText.color;
            c.a = alpha;
            uiText.color = c;
        }
    }
}