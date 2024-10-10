using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    

    private float randomRotationSpeedMin = 20f;
    private float randomRotationSpeedMax = 200f;
    // Start is called before the first frame update
    void Start()
    {
        // Change the angle at which the cube rotates
        transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the cube based on the random rotation speed
        transform.Rotate(Vector3.up, Time.deltaTime * GetRandomRotationSpeed());
    }

    // Get a random rotation speed
    float GetRandomRotationSpeed()
    {
        return Random.Range(randomRotationSpeedMin, randomRotationSpeedMax);
    }
}
