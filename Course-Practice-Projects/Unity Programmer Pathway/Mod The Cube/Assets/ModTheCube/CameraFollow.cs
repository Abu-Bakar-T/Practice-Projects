using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object the camera will follow
    public float smoothTime = 0.3f; // Smoothing time for camera movement
    private float locMultipler;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        InvokeRepeating("LocMultipler", 0, 5);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 2, -5)* locMultipler);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // Make the camera look at the target
            transform.LookAt(target);
        }
    }

    void LocMultipler()
    {
        locMultipler = Random.Range(1, 5);
    }
}