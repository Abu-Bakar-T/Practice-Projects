using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Variables for randomization
    private float randomScaleMin = 0.5f;
    private float randomScaleMax = 2.0f;
    private float randomColorChangeIntervalMin = 2f;
    private float randomColorChangeIntervalMax = 5f;

    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        delay = Random.Range(randomColorChangeIntervalMin, randomColorChangeIntervalMax);

        // Change the cube's location (transform)
        transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), Random.Range(-10f, 10f));
        
        // Using Invoke Repeating to change the color over time
        InvokeRepeating("ChangeColorOverTime", 0, delay);

        // Using Invoke Repeating to Randomize initial properties over time
        InvokeRepeating("RandomizeCube", 0, delay);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Randomize initial properties
        //RandomizeCube();
    }

    // Randomize cube properties
    void RandomizeCube()
    {
        

        // Change the cube's scale
        float randomScale = Random.Range(randomScaleMin, randomScaleMax);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);      

        // Change the cube’s material color
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

        // Change the cube’s material opacity
        Color newColor = GetComponent<Renderer>().material.color;
        newColor.a = Random.Range(0.2f, 1.0f);
        GetComponent<Renderer>().material.color = newColor;
    }
    

    // Coroutine to change the color over time
    public void ChangeColorOverTime()
    {
        delay = Random.Range(randomColorChangeIntervalMin, randomColorChangeIntervalMax);
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
}