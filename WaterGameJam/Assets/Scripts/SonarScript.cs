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
    private float underwaterPingsSpeed = 5f;
    private float abovewaterPingsSpeed = 20f;
    private List<GameObject> underwaterWalkPings = new List<GameObject>();
    private List<GameObject> abovewaterWalkPings = new List<GameObject>();
    private List<GameObject> underwaterSneakPings = new List<GameObject>();
    private List<GameObject> abovewaterSneakPings = new List<GameObject>();
    private List<GameObject> underwaterPings = new List<GameObject>();
    private List<GameObject> abovewaterPings = new List<GameObject>();

    [SerializeField] private GameObject underwaterPingPrefab;
    [SerializeField] private GameObject abovewaterPingPrefab;
    private float pingSizeDeathThreshold = 30f;
    private float pingSizeDeathThresholdSneaking = 5f;
    private float pingSizeDeathThresholdWalking = 10f;

    private bool playerIsSneaking = false;

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
        abovewaterPings.Add(Instantiate(abovewaterPingPrefab, this.transform.position += new Vector3(0, 25.25f, 0), Quaternion.identity));
        underwaterPings.Add(Instantiate(underwaterPingPrefab, this.transform.position -= new Vector3(0, 50.5f, 0), Quaternion.identity));
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

    private void MoveSneakingPings()
    {
        try
        {
            foreach (GameObject ping in underwaterSneakPings)
            {
                // increase scale of underwater pings
                ping.transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * underwaterPingsSpeed;
            }
        }
        catch
        {
            Debug.Log("Tried to move underwater sneak pings, but list was likely empty");
        }
        
        try
        {
            foreach (GameObject ping in abovewaterSneakPings)
            {
                ping.transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * abovewaterPingsSpeed;
            }
        }
        catch
        {
            Debug.Log("Tried to move abovewater sneak pings, but list was likely empty");
        }
    }

    private void DestroySneakingPings()
    {
        for (int i = underwaterSneakPings.Count - 1; i >= 0; i--)
        {
            if (underwaterSneakPings[i].transform.localScale.x >= pingSizeDeathThresholdSneaking)
            {
                Destroy(underwaterSneakPings[i]);
                underwaterSneakPings.RemoveAt(i);
            }
        }

        for (int i = abovewaterSneakPings.Count - 1; i >= 0; i--)
        {
            if (abovewaterSneakPings[i].transform.localScale.x >= pingSizeDeathThresholdSneaking)
            {
                Destroy(abovewaterSneakPings[i]);
                abovewaterSneakPings.RemoveAt(i);
            }
        }
    }

    private void MoveWalkingPings()
    {
        try
        {
            foreach (GameObject ping in underwaterWalkPings)
            {
                // increase scale of underwater pings
                ping.transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * underwaterPingsSpeed;
            }
        }
        catch
        {
            Debug.Log("Tried to move underwater sneak pings, but list was likely empty");
        }
        
        try
        {
            foreach (GameObject ping in abovewaterWalkPings)
            {
                ping.transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * abovewaterPingsSpeed;
            }
        }
        catch
        {
            Debug.Log("Tried to move abovewater sneak pings, but list was likely empty");
        }
    }

    private void DestroyWalkingPings()
    {
        for (int i = underwaterWalkPings.Count - 1; i >= 0; i--)
        {
            if (underwaterSneakPings[i].transform.localScale.x >= pingSizeDeathThresholdWalking)
            {
                Destroy(underwaterWalkPings[i]);
                underwaterWalkPings.RemoveAt(i);
            }
        }

        for (int i = abovewaterWalkPings.Count - 1; i >= 0; i--)
        {
            if (abovewaterSneakPings[i].transform.localScale.x >= pingSizeDeathThresholdWalking)
            {
                Destroy(abovewaterWalkPings[i]);
                abovewaterWalkPings.RemoveAt(i);
            }
        }
    }
}