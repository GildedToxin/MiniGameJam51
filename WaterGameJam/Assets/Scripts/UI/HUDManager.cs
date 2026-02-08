using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject interact;
    public GameObject turn;
    public GameObject slider;
    public GameObject ping;
    public GameObject oxygen;
    public GameObject deadPanel;

    //public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;

    private void Start()
    {
        interact.SetActive(false);
        turn.SetActive(false);
        ping.SetActive(false);
        oxygen.SetActive(false);

        GameManager.Instance.hud = this;
    }
    public void PlayerHurt()
    {
        deadPanel.GetComponent<Image>().color = new Color(deadPanel.GetComponent<Image>().color .r, deadPanel.GetComponent<Image>().color.g, deadPanel.GetComponent<Image>().color.b, 1f); // 100% visible
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            deadPanel.GetComponent<Image>().color = new Color(deadPanel.GetComponent<Image>().color.r, deadPanel.GetComponent<Image>().color.g, deadPanel.GetComponent<Image>().color.b, Mathf.Lerp(1f, 0f, time / fadeDuration));
            yield return null;
        }

        deadPanel.GetComponent<Image>().color = new Color(deadPanel.GetComponent<Image>().color.r, deadPanel.GetComponent<Image>().color.g, deadPanel.GetComponent<Image>().color.b, 0f);
    }
}
