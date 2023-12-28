using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float radius = 3.0f; // Radius of the circular path
    public float speed = 2.0f; // Rotation speed
    public float startAngleOffset = 0.0f; // Offset to ensure objects start at different positions

    private float angle = 0.0f;

    void Start()
    {
        // Offset the initial angle based on the startAngleOffset
        angle = startAngleOffset;
    }

    void Update()
    {
        // Calculate the new position in a circular path
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Update the object's local position (relative to the parent)
        transform.localPosition = new Vector3(x, transform.localPosition.y, z);

        // Increment the angle based on speed
        angle += speed * Time.deltaTime;

        // Ensure the angle stays within the 0 to 360-degree range
        if (angle > 360.0f)
        {
            angle -= 360.0f;
        }
    }
}