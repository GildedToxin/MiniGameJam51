using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class SonarScript : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    private PlayerController playerController;
    private float waterHeight = 0f;

    [Header("Ping Settings")]
    [SerializeField] private float underwaterPingsSpeed = 2.5f;
    [SerializeField] private float abovewaterPingsSpeed = 5f;
    private List<GameObject> underwaterPings = new List<GameObject>();
    private List<GameObject> abovewaterPings = new List<GameObject>();
    [SerializeField] private GameObject underwaterPingPrefab;
    [SerializeField] private GameObject abovewaterPingPrefab;

    void Start()
    {
        playerTransform = player.transform;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        FollowPlayer();

        if (playerController.sonarEffect)
        {
            SonarPing();
        }

        if (underwaterPings.Count > 0 || abovewaterPings.Count > 0)
        {
            MovePings();
        }
    }
    private void FollowPlayer()
    {
        Vector3 playerPosition = playerTransform.position;
        this.transform.position = new Vector3(playerPosition.x, waterHeight, this.transform.position.z);
    }

    private void SonarPing()
    {
        abovewaterPings.Add(Instantiate(abovewaterPingPrefab, this.transform.position += new Vector3(0, 10, 0), Quaternion.identity));
        underwaterPings.Add(Instantiate(underwaterPingPrefab, this.transform.position += new Vector3(0, 10, 0), Quaternion.identity));
        playerController.sonarEffect = false;
    }

    private void MovePings()
    {
        try
        {
            foreach (GameObject ping in underwaterPings)
            {
                // increase scale of underwater pings
                ping.transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * underwaterPingsSpeed;
            }
        }
        catch
        {
            Debug.Log("Tried to move underwater pings, but list was likely empty");
        }
        
        try
        {
            foreach (GameObject ping in abovewaterPings)
        {
            ping.transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * abovewaterPingsSpeed;
        }
        }
        catch
        {
            Debug.Log("Tried to move abovewater pings, but list was likely empty");
        }
    }
}