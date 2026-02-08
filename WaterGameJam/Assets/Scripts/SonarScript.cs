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
    private float underwaterPingsSpeed = 7f;
    private float abovewaterPingsSpeed = 15f;
    private List<GameObject> underwaterWalkPings = new List<GameObject>();
    private List<GameObject> abovewaterWalkPings = new List<GameObject>();
    private List<GameObject> underwaterSneakPings = new List<GameObject>();
    private List<GameObject> abovewaterSneakPings = new List<GameObject>();
    private List<GameObject> underwaterPings = new List<GameObject>();
    private List<GameObject> abovewaterPings = new List<GameObject>();

    [SerializeField] private GameObject underwaterPingPrefab;
    [SerializeField] private GameObject abovewaterPingPrefab;
    private float pingSizeDeathThreshold = 30f;
    private float pingSizeDeathThresholdSneaking = 2.5f;
    private float pingSizeDeathThresholdWalking = 5f;

    private float walkCooldown = 0.5f;
    private float walkTimer = 0f;
    private float autoPingCooldown = 10f;
    private float autoPingTimer = 0f;

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
        DestroySneakingPings();
        DestroyWalkingPings();

        if (walkTimer > 0)
        {
            walkTimer -= Time.deltaTime;
            Mathf.Clamp(walkTimer, 0, walkCooldown);
        }

        PingOnWalk();
        TimerPing();
    }

    void FixedUpdate()
    {
        if (underwaterPings.Count > 0 || abovewaterPings.Count > 0)
        {
            MovePings();
        }

        if (underwaterSneakPings.Count > 0 || abovewaterSneakPings.Count > 0)
        {
            MoveSneakingPings();
        }

        if (underwaterWalkPings.Count > 0 || abovewaterWalkPings.Count > 0)
        {
            MoveWalkingPings();
        }
    }

    private void FollowPlayer()
    {
        Vector3 playerPosition = playerTransform.position;
        this.transform.position = new Vector3(playerPosition.x, waterHeight, playerPosition.z);
    }

    private void PlayPingSFX()
    {
       // player.transform.GetChild(2).GetComponent<AudioSource>().Play(pingSFX);
    }
    private void SonarPing()
    {
        //Player

        PlayPingSFX();
        abovewaterPings.Add(Instantiate(abovewaterPingPrefab, this.transform.position += new Vector3(0, 25.25f, 0), Quaternion.identity));
        underwaterPings.Add(Instantiate(underwaterPingPrefab, this.transform.position -= new Vector3(0, 50.5f, 0), Quaternion.identity));
        playerController.sonarEffect = false;
    }

    private void PingOnWalk()
    {
        //Walking

        PlayPingSFX();
        if (playerController.isMoving && walkTimer <= 0)
        {
            walkTimer = walkCooldown;
            if (playerIsSneaking)
            {
                abovewaterSneakPings.Add(Instantiate(abovewaterPingPrefab, this.transform.position += new Vector3(0, 25.25f, 0), Quaternion.identity));
                underwaterSneakPings.Add(Instantiate(underwaterPingPrefab, this.transform.position -= new Vector3(0, 50.5f, 0), Quaternion.identity));
            }
            else
            {
                abovewaterWalkPings.Add(Instantiate(abovewaterPingPrefab, this.transform.position += new Vector3(0, 25.25f, 0), Quaternion.identity));
                underwaterWalkPings.Add(Instantiate(underwaterPingPrefab, this.transform.position -= new Vector3(0, 50.5f, 0), Quaternion.identity));
            }
        }
    }

    private void TimerPing()
    {
        //Random

        PlayPingSFX();
        autoPingTimer -= Time.deltaTime;
        if (autoPingTimer <= 0)
        {
            abovewaterPings.Add(Instantiate(abovewaterPingPrefab, this.transform.position += new Vector3(0, 25.25f, 0), Quaternion.identity));
            underwaterPings.Add(Instantiate(underwaterPingPrefab, this.transform.position -= new Vector3(0, 50.5f, 0), Quaternion.identity));
            autoPingTimer = autoPingCooldown;
        }
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
            if (underwaterWalkPings[i].transform.localScale.x >= pingSizeDeathThresholdWalking)
            {
                Destroy(underwaterWalkPings[i]);
                underwaterWalkPings.RemoveAt(i);
            }
        }

        for (int i = abovewaterWalkPings.Count - 1; i >= 0; i--)
        {
            if (abovewaterWalkPings[i].transform.localScale.x >= pingSizeDeathThresholdWalking)
            {
                Destroy(abovewaterWalkPings[i]);
                abovewaterWalkPings.RemoveAt(i);
            }
        }
    }
}