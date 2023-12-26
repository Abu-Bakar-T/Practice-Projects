using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindView_Camera_Follow : MonoBehaviour
{
    public GameObject Car;
    private Vector3 offset;
    private bool wasLookingBackward = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        float forwardMovement = Vector3.Dot(Car.transform.forward, Vector3.forward);
        bool lookBackward = forwardMovement < 0;

        if (lookBackward & !wasLookingBackward)
        {
            offset = new Vector3(0f, offset.y, -offset.z);
            wasLookingBackward = true;
        }
        else if (!lookBackward)
        {
            wasLookingBackward = false;
            offset = new Vector3(0, 7, -10);
        }
        transform.position = Car.transform.position + offset;

        // Lookat command ensures that camera will always follow the object added to Lookat.
        transform.LookAt(Car.transform.position + Car.transform.forward * 5f);
    }
}