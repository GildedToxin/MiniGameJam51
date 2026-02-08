using UnityEngine;

public class Mine : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float bobHeight = 0.5f;
    public float bobSpeed = 1f;

    [Range(0f, 0.5f)]
    public float variation = 0.3f;

    private Vector3 startPosition;
    private float bobOffset;

    private float actualRotationSpeed;
    private float actualBobSpeed;
    private float actualBobHeight;

    public AudioSource audioSource;

    void Start()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            Random.Range(0f, 360f),
            transform.rotation.eulerAngles.z
        );

        actualRotationSpeed = rotationSpeed * Random.Range(1f - variation, 1f + variation);
        actualBobSpeed = bobSpeed * Random.Range(1f - variation, 1f + variation);
        actualBobHeight = bobHeight * Random.Range(1f - variation, 1f + variation);

        bobOffset = Random.Range(0f, Mathf.PI * 2f);

        startPosition = transform.position + Vector3.up * Random.Range(-actualBobHeight, actualBobHeight);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, actualRotationSpeed * Time.deltaTime, Space.World);

        float newY =
            startPosition.y +
            Mathf.Sin(Time.time * actualBobSpeed + bobOffset) * actualBobHeight;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            GameManager.Instance.KillPlayer();
            //Destroy(gameObject);
        }
    }
}
