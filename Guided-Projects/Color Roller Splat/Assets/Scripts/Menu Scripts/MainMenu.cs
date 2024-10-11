using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _canvas;
    //[SerializeField] private GameObject _levelCompleteCanvas;
    //[SerializeField] private BallRoadPainter _ballRoadPainter;

    private void Start()
    {
        _canvas?.SetActive(false);
    }
    /*
    <Summary>
    A function that is to be used with button to load next scene
    </Summary>
     */
    public void NextScene()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync(++currentLevelIndex);
        }
    }

    /*
    <Summary>
    A function that is to be used with button to exit from the application
    </Summary>
     */
    public void Quit()
    {
        Application.Quit(); 
    }


    /*
   <Summary>
   A function that is to be used with button to pause the game
   </Summary>
    */
    public void Pause()
    {
        Time.timeScale = 0f;
        _pauseButton?.SetActive(false);
        _canvas?.SetActive(true);
    }

    /*
   <Summary>
   A function that is to be used with button to Continue the paused game
   </Summary>
    */
    public void Continue()
    {
        Time.timeScale = 1f;
        _pauseButton?.SetActive(true);
        _canvas?.SetActive(false);
    }

    /*
   <Summary>
   A function that is to be used with button to load Main Menu
   </Summary>
    */
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}
