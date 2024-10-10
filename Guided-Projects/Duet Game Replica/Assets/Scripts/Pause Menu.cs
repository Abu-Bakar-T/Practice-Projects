using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas pauseCanvas;
    public void Pause()
    {
        Time.timeScale = 0;
        pauseCanvas.gameObject.SetActive(true);
    }

    public void Continue()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
