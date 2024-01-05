using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int lives;
    public bool isGameActive;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;

    private void Start() 
    {
        isGameActive = true;
        lives = 3;
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        Debug.Log("Score: " + score);
    }

    public void IncreaseScore()
    {
        score+=10; 
        scoreText.text = "Score: " + score;
        if(score % 100 == 0)
        {
            lives += 1;
        }
        Debug.Log("Score: "+score);
    }

    public void DecreaseLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives;
        Debug.Log("Lives: " + lives);
        if(lives < 1)
        {
            Time.timeScale = 0;
            isGameActive = false;
            gameOverText.gameObject.SetActive(true);
        }
    }
}
