using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button play;
    public Button quit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //  EventSystem.current.SetSelectedGameObject(null); // clear old selection
      //  EventSystem.current.SetSelectedGameObject(newGame.gameObject);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("Station Kadia");
        //Destroy(GameManager.Instance.gameObject);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
