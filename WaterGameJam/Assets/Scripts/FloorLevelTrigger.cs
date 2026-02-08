using UnityEngine;

public class FloorLevelTrigger : MonoBehaviour
{
    PlayerController playerController;
    public int oldFloor = 0;
    public int newFloor = 1;

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerController.currentFloor == oldFloor)
        {
            playerController.currentFloor = newFloor;
        }
        else if (playerController.currentFloor == newFloor)
        {
            playerController.currentFloor = oldFloor;
        }
    }
}
