using GG.Infrastructure.Utils.Swipe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _swipeListener;
    public GameObject LevelCompleteCanvas;

    private void Start()
    {
        LevelCompleteCanvas.SetActive(false);
    }

    private void Update()
    {
        if (BallRoadPainter.Instance.LevelCompleted)
        {
            _swipeListener.SetActive(false);
            _pauseButton.SetActive(false);
            LevelCompleteCanvas.SetActive(true);
        }
    }
}
