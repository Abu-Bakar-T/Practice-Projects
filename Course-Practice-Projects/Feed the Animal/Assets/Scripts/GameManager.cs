using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    private int lives;

    private void Start() 
    {
        lives = 3;
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        Debug.Log("Score: " + score);
    }

    private void Update()
    {
        
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
            gameOverText.gameObject.SetActive(true);
        }
    }
}
