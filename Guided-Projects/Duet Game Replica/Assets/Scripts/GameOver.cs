using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] Canvas gameOver;
    public void ActiveGameOver()
    {
        Time.timeScale = 0;
        gameOver.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Debug.Log("Loading Level0");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Level0");
    }
    public void MainMenu()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
