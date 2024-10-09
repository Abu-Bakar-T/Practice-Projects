using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WinUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private Button restartButton;
    [SerializeField] private AudioSource SFX;

    [Header("Board Reference: ")]
    [SerializeField] private Board board;

    void Start()
    {
        restartButton.onClick.AddListener(() => StartCoroutine(RestartGame()));
        if (board != null)
        {
            board.onWinAction += OnWinEvent;
        }
        canvas.SetActive(false);
    }

    private IEnumerator RestartGame()
    {
        // Play the sound effect
        SFX.Play();

        // Wait for the sound effect to finish
        yield return new WaitForSeconds(SFX.clip.length);

        // Load the scene
        SceneManager.LoadScene(0);
    }

    private void OnWinEvent(Mark mark, Color color)
    {
        if( mark == Mark.None )
        {
            winnerText.text = "Nobody Wins";
            Debug.Log("Nobody Wins"); // Debug to ensure this condition works
        }
        else
        {
            winnerText.text = mark.ToString() + " Wins";
            Debug.Log(mark.ToString() + " Wins"); // Debug to check if a valid mark is being set
        }
        color.a = 1.0f;
        winnerText.color = color;
        canvas.SetActive(true);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveAllListeners();
        board.onWinAction -= OnWinEvent;
    }
}