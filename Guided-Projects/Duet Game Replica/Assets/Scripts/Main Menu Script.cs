using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void NextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadSceneAsync(++currentLevelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
