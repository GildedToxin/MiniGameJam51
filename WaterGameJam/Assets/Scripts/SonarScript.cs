using UnityEngine;
using UnityEngine.InputSystem;

public class SonarScript : MonoBehaviour
{
    public Transform playerTransform;
    private float waterHeight = 0f;

    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        Vector3 playerPosition = playerTransform.position;
        this.transform.position = new Vector3(playerPosition.x, waterHeight, this.transform.position.z);
    }
}
