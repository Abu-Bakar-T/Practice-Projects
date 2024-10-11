using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallRoadPainter : MonoBehaviour
{
    public static BallRoadPainter Instance;
    [Header("References: ")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private BallMovement ballMovement;
    [SerializeField] private MeshRenderer ballMeshRenderer;
    

    public int paintedRoadTiles = 0;
    public bool LevelCompleted = false;
    void Start()
    {
        Instance = this;
        LevelCompleted = false;
        ballMeshRenderer.material.color = levelManager.paintColor;

        Paint(levelManager.defaultBallRoadTile, .5f, 0f);

        ballMovement.onMoveStart += OnBallMoveStartHandler;
    }

    private void OnBallMoveStartHandler(List<RoadTile> roadTiles, float totalDuration)
    {
        float stepDuration = totalDuration / roadTiles.Count;
        for (int i = 0; i < roadTiles.Count; i++)
        {
            RoadTile roadTile = roadTiles[i];
            if (!roadTile.isPainted)
            {
                float duration = totalDuration / 2f;
                float delay = i * (stepDuration / 2f);
                Paint(roadTile, duration, delay);

                // check if level completed
                if (paintedRoadTiles == levelManager.roadTilesList.Count)
                {
                    LevelCompleted = true;
                    Debug.Log("Level Completed");
                    // Load new level
                }
            }
        }
    }

    private void Paint(RoadTile roadTile, float duration, float delay)
    {
        roadTile.meshRenderer.material.DOColor(levelManager.paintColor, duration).SetDelay(delay);
        roadTile.isPainted = true;
        paintedRoadTiles++;
    }
}
