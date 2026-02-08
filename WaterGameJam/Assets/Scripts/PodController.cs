using System.Diagnostics;
using UnityEngine;

public class PodController : MonoBehaviour, IPlayerLookTarget
{

    public bool isLookedAt = false;
    public bool isInteracted = false;
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

        // End The Game Here
    }
    public void StopInteract()
    {

    }
}
