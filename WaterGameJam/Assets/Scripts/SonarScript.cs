using UnityEngine;
using UnityEngine.InputSystem;

public class SonarScript : MonoBehaviour
{
    private float spreadAngle = 30f;
    private float maxDistance = 20f;
    private int numberOfRays = 30;
    public GameObject pingPrefab;

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            EmitSonar();
            Debug.Log("Sonar Emitted");
        }
    }

    private void EmitSonar()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            Vector3 direction = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle,spreadAngle), 0) * transform.forward;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistance))
            {
                Instantiate(pingPrefab, hit.point, hit.normal == Vector3.up ? Quaternion.identity : Quaternion.LookRotation(hit.normal));
                Debug.Log(hit.normal);
            }
        }
    }
}
