using System.Diagnostics;
using UnityEngine;

public class PodController : MonoBehaviour, IPlayerLookTarget
{

    public bool isLookedAt = false;
    public bool isInteracted = false;

    public GameObject go;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnLookEnter()
    {
        isLookedAt = true;
        GameManager.Instance.hud.interact.SetActive(true);
    }
    public void OnLookExit()
    {
        isLookedAt = false;
        GameManager.Instance.hud.interact.SetActive(false);
    }
    public void Interact()
    {
        isInteracted = true;
        go.SetActive(true);
        //Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        // End The Game Here
    }
    public void StopInteract()
    {

    }
}
