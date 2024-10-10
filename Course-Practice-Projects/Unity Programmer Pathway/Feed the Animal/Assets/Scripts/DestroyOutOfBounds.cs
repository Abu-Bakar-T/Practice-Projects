using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] float topBound = 30;
    [SerializeField] float lowerBound = -10;
    [SerializeField] float leftBound = -25;
    [SerializeField] float rightBound = 25;
    [SerializeField] GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isGameActive)
        {
            if (transform.position.z > topBound)
            {
                // Just deactivate it instead of Destroying the Object
                gameObject.SetActive(false);

            }
            else if (transform.position.z < lowerBound || transform.position.x > rightBound || transform.position.x < leftBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
