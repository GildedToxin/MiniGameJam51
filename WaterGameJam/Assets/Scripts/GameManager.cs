using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;

    public  List<GameObject> enemies = new List<GameObject>();


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
}
