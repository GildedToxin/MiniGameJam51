using UnityEngine;

public class SpawnMines : MonoBehaviour
{
    public int waterLevelToSpawn;
    public bool hasSpawned = false;

    // Update is called once per frame
    private void Start()
    {
        if (!hasSpawned)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    void Update()
    {
        if(!hasSpawned && GameManager.Instance.currentWaterLevel == waterLevelToSpawn)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            hasSpawned = true;
        }
    }
}
