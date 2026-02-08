using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;

    public  List<GameObject> enemies = new List<GameObject>();

    public int currentWaterLevel;

    public HUDManager hud;

    public WaterLevel waterLevel;
    public SonarScript sonarScript;


    public Valve lastTurnedValve;

    public List<DialogueGroup> valveGroups = new List<DialogueGroup>();

    public GameObject door;

    public GameObject firstEnemy;
    public GameObject secondEnemy;
    private bool spawnFirstEnemy = false;
    private bool spawnSecondEnemy = false;

    private float enemySpawnTimer = 0f;
    private float enemySpawnCooldown = 20f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        Cursor.lockState = CursorLockMode.Confined;
       // Cursor.visible = false;

    }

    void Update()
    {
        if (currentWaterLevel == 1 && !spawnFirstEnemy)
        {
            Invoke("SpawnEnemy", 20f);
            spawnFirstEnemy = true;
        }

        if (currentWaterLevel == 2 && !spawnSecondEnemy)
        {
            Invoke("SpawnEnemy", 20f);
            spawnSecondEnemy = true;
        }
    }

    private void SpawnEnemy()
    {
        if (currentWaterLevel == 1)
        {
            firstEnemy.SetActive(true);
        }
        else if (currentWaterLevel == 5)
        {
            secondEnemy.SetActive(true);
        }
    }


    private void Start()
    {
        DialogueManager.Instance.PlayDialogueSequence(GetComponent<GameIntroDialogue>().introLines);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SonarPing(Vector3 location)
    {
        print("sonar detected at " + location);
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyController ec = enemy.GetComponent<EnemyController>();
                if (ec != null)
                {
                    ec.ReceiveSonarPing(location);
                }
            }
        }
    }
    public void RegisterEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void KillPlayer()
    {
        hud.PlayerHurt();
        player.KillPlayer();
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        if(lastTurnedValve == null)
        {
            UnityEngine.Debug.LogWarning("No last turned valve set for respawn!");
            return;
        }
        player.transform.position = lastTurnedValve.transform.GetChild(3).position;
        player.transform.rotation = lastTurnedValve.transform.GetChild(3).rotation;
    }

    [ContextMenu("Increase Water Level")]
    public void IncreaseWaterLevel()
    {
        currentWaterLevel++;

        DialogueManager.Instance.PlayDialogueSequence(valveGroups[currentWaterLevel - 1]);
        waterLevel.IncreaseWaterLevel(currentWaterLevel);
        sonarScript.SetWaterHeight(waterLevel.values2[currentWaterLevel]);
    }
}
