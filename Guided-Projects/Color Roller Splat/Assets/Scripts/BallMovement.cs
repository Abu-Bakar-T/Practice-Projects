using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using DG.Tweening;
using UnityEngine.Events;

// Library to sort tiles by distance from ray's orgin using LINQ Queries (Line 55)
using System.Linq;

public class BallMovement : MonoBehaviour
{
    [Header("References: ")]
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private float stepDuration = 0.1f;
    [SerializeField] private LayerMask wallsAndRoadsLayer;
    [SerializeField] private const float MAX_RAY_DISTANCE = 100f;
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioSource ballMovement;
    [SerializeField] private AudioClip ballAudio;

    public UnityAction<List<RoadTile>, float> onMoveStart;

    private Vector3 moveDirection;
    private bool canMove = true;

    void Start()
    {
        transform.position = levelManager.defaultBallRoadTile.position;

        swipeListener.OnSwipe.AddListener(swipe =>
        {
            switch (swipe)
            {
                case "Right":
                    moveDirection = Vector3.right;
                    break;
                case "Left":
                    moveDirection = Vector3.left;
                    break;
                case "Up":
                    moveDirection = Vector3.forward;
                    break;
                case "Down":
                    moveDirection = Vector3.back;
                    break;
            }
            SFX.Play();
            MoveBall();
        });
    }

    private void MoveBall()
    {
        if(canMove)
        {
            // adding raycast in swipe direction from the ball
            RaycastHit[] hits = Physics.RaycastAll(transform.position, moveDirection, MAX_RAY_DISTANCE, wallsAndRoadsLayer.value).OrderBy(hit=>hit.distance).ToArray();

            Vector3 targetPosition = transform.position;
            int steps = 0;

            List<RoadTile> pathRoadTiles = new List<RoadTile>();
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.isTrigger)
                {
                    // Road Tile, add it to the list to be painted
                    pathRoadTiles.Add(hits[i].transform.GetComponent<RoadTile>());
                }
                else
                {
                    // Wall tile
                    if(i == 0)
                    {
                        // wall is near ball
                        canMove = true;
                        return;
                    }
                    steps = i;
                    targetPosition = hits[i-1].transform.position;
                    break;
                }
            }

            // Move ball to target Position
            ballMovement.PlayOneShot(ballAudio);
            float moveDuration = stepDuration * steps;
            transform.DOMove(targetPosition, moveDuration).SetEase(Ease.OutExpo).OnComplete(() => canMove = true);
            if(onMoveStart != null)
            {
                onMoveStart.Invoke(pathRoadTiles, moveDuration);
            }
        }
    }
}
