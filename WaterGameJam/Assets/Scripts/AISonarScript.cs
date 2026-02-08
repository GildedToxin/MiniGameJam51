using UnityEngine;
using System.Collections.Generic;

public class AISonarScript : MonoBehaviour
{
    private float waterHeight = 9f;

    private float underwaterPingsSpeed = 10f;
    private float abovewaterPingsSpeed = 15f;
    private float pingSizeDeathThreshold = 15f;

    private List<GameObject> aiPingsAboveWater = new List<GameObject>();
    private List<GameObject> aiPingsUnderWater = new List<GameObject>();
    [SerializeField] private GameObject aiPingPrefabAboveWater;
    [SerializeField] private GameObject aiPingPrefabUnderWater;

    void Start()
    {
        
    }

    void Update()
    {
        MovePings();
        DestroyPings();
    }

    private void MovePings()
    {
        try
        {
            foreach (GameObject ping in aiPingsUnderWater)
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
            foreach (GameObject ping in aiPingsAboveWater)
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
        for (int i = aiPingsUnderWater.Count - 1; i >= 0; i--)
        {
            if (aiPingsUnderWater[i].transform.localScale.x >= pingSizeDeathThreshold)
            {
                Destroy(aiPingsUnderWater[i]);
                aiPingsUnderWater.RemoveAt(i);
            }
        }

        for (int i = aiPingsAboveWater.Count - 1; i >= 0; i--)
        {
            if (aiPingsAboveWater[i].transform.localScale.x >= pingSizeDeathThreshold)
            {
                Destroy(aiPingsAboveWater[i]);
                aiPingsAboveWater.RemoveAt(i);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sonar"))
        {
            aiPingsAboveWater.Add(Instantiate(aiPingPrefabAboveWater, this.transform.position += new Vector3(0, 25.25f + waterHeight - this.transform.position.y, 0), Quaternion.identity));
            aiPingsUnderWater.Add(Instantiate(aiPingPrefabUnderWater, this.transform.position -= new Vector3(0, 16.25f + this.transform.position.y, 0), Quaternion.identity));
        }
    }
}
