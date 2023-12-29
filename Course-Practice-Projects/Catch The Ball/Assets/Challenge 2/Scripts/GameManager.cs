using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private int lives = 4;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    public void IncreaseScore()
    {
        score = score + 1;
        scoreText.text = "Score: " + score;
    }
    public void decreaseLives()
    {
        lives = lives - 1;
        livesText.text = "Lives: " + lives;
        if(lives <= 0)
        {
            Time.timeScale = 0;
            gameOverText.gameObject.SetActive(true);
        }
    }
}
