using UnityEngine;
using UnityEngine.UI;

public class Valve : MonoBehaviour, IPlayerLookTarget
{
    public bool isLookedAt; // Whether the player is currently looking at the valve
    public bool isInteracting; // Whether the player is currently looking at the valve
    public bool isTurned; // Whether the valve has been fully turned
    public float turnPrecentage;
    public float turnSpeed;

    public float looseSpeed;

    public float valveIndex;

    public GameObject valveHandle;
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
        }
    public void Interact()
    {
        isInteracting = true;
    }
    public void StopInteract()
    {
        isInteracting = false;
    }

    public void Update()
    {
        if (isTurned) return;

        if (isInteracting && isLookedAt)
        {
            float degreesPerSecond = 360f / 10;
            float delta = degreesPerSecond * Time.deltaTime;
            valveHandle.transform.Rotate(Vector3.forward, delta, Space.Self);


            turnPrecentage += turnSpeed * Time.deltaTime;
            GameManager.Instance.hud.slider.GetComponent<Slider>().value = turnPrecentage;
        }
        else if (turnPrecentage > 0 && turnPrecentage < 10)
        {
            turnPrecentage = 0;
            GameManager.Instance.hud.slider.GetComponent<Slider>().value = turnPrecentage;
        }
        
        if(turnPrecentage >= 10)
        {
            isTurned = true;
            GameManager.Instance.hud.turn.SetActive(false);
            GameManager.Instance.IncreaseWaterLevel();
        }

            turnPrecentage = Mathf.Clamp(turnPrecentage, 0, 10);
    }
}
