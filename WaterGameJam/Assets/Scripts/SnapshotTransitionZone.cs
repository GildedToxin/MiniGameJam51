using UnityEngine;

public class SnapshotTransitionZone : MonoBehaviour
{
    PlayerController playerController;

    public void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerController.isUnderwater)
            {
                playerController.isUnderwater = false;
            }
            else if (!playerController.isUnderwater)
            {
                playerController.isUnderwater = true;
            }
        }
    }
}
