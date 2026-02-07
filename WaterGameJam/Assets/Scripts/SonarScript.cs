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
    private float waterHeight = 9f;

    [Header("Ping Settings")]
    private float underwaterPingsSpeed = 30f;
    private float abovewaterPingsSpeed = 60f;
    private List<GameObject> underwaterPings = new List<GameObject>();
    private List<GameObject> abovewaterPings = new List<GameObject>();
    [SerializeField] private GameObject underwaterPingPrefab;
    [SerializeField] private GameObject abovewaterPingPrefab;
    private float pingSizeDeathThreshold = 100f;

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

        DestroyPings();
    }

    void FixedUpdate()
    {
        if (underwaterPings.Count > 0 || abovewaterPings.Count > 0)
        {
            MovePings();
        }
    }

    private void FollowPlayer()
    {
        Vector3 playerPosition = playerTransform.position;
        this.transform.position = new Vector3(playerPosition.x, waterHeight, playerPosition.z);
    }

    private void SonarPing()
    {
        abovewaterPings.Add(Instantiate(abovewaterPingPrefab, this.transform.position += new Vector3(0, 20, 0), Quaternion.identity));
        underwaterPings.Add(Instantiate(underwaterPingPrefab, this.transform.position -= new Vector3(0, 40, 0), Quaternion.identity));
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

    private void DestroyPings()
    {
        for (int i = underwaterPings.Count - 1; i >= 0; i--)
        {
            if (underwaterPings[i].transform.localScale.x >= pingSizeDeathThreshold)
            {
                Destroy(underwaterPings[i]);
                underwaterPings.RemoveAt(i);
            }
        }

        for (int i = abovewaterPings.Count - 1; i >= 0; i--)
        {
            if (abovewaterPings[i].transform.localScale.x >= pingSizeDeathThreshold)
            {
                Destroy(abovewaterPings[i]);
                abovewaterPings.RemoveAt(i);
            }
        }
    }
}