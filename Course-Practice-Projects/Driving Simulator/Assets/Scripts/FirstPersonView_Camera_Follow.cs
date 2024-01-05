using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView_Camera_Follow : MonoBehaviour
{
    public GameObject Car;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset.x = -0.55f;
        offset.y = 0.55f;
        offset.z = 0.7f;
    }

    private void LateUpdate()
    {
        //TransformDirection Command converts the offset to values car's local space, taking acount into its rotation
        transform.position = Car.transform.position + Car.transform.TransformDirection(offset);

        // Lookat command ensures that camera will always follow the object added to Lookat.
        transform.LookAt(Car.transform.position + Car.transform.forward * 5f);
    }
}
