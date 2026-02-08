using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float distanceToMove = 10f;
    public AudioSource audioSource;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        audioSource.Play();
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= distanceToMove)
        {
            Destroy(gameObject);
        }
    }
}
