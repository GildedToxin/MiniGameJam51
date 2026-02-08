using UnityEngine;
using System.Collections.Generic;

public class MineSonarScript : MonoBehaviour
{
    private float pingTimer = 0f;
    private float pingCooldown = 10f;
    private List<GameObject> minePings = new List<GameObject>();
    [SerializeField] private GameObject minePingPrefab;
    private float pingSpeed = 3f;
    private float pingSizeDeathThreshold = 2f;

    void Start()
    {
        pingTimer = Random.Range(2, pingCooldown-2f);
    }

    void Update()
    {
        TimerPing();
        MovePings();
        DestroyPings();
    }

    private void TimerPing()
    {
        pingTimer -= Time.deltaTime;
        Mathf.Clamp(pingTimer, 0, pingCooldown);
        if (pingTimer <= 0)
        {
            minePings.Add(Instantiate(minePingPrefab, this.transform.position, Quaternion.identity));
            pingTimer = pingCooldown;
        }
    }

    private void MovePings()
    {
        try
        {
            foreach (GameObject ping in minePings)
            {
                // increase scale of underwater pings
                ping.transform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * pingSpeed;
            }
        }
        catch
        {
            Debug.Log("Tried to move underwater pings, but list was likely empty");
        }
    }

    private void DestroyPings()
    {
        for (int i = minePings.Count - 1; i >= 0; i--)
        {
            if (minePings[i].transform.localScale.x >= pingSizeDeathThreshold)
            {
                Destroy(minePings[i]);
                minePings.RemoveAt(i);
            }
        }
    }
}
