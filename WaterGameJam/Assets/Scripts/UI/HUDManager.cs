using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject interact;
    public GameObject turn;
    public GameObject slider;
    public GameObject ping;
    public GameObject oxygen;

    private void Start()
    {
        interact.SetActive(false);
        turn.SetActive(false);
        ping.SetActive(false);
        oxygen.SetActive(false);

        GameManager.Instance.hud = this;
    }
}
