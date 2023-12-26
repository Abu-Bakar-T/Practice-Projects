using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public GameObject Car;
    private Vector3 offset = new Vector3(0, 7, -10);
    public float rotation_xoffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Car.transform.rotation;
        transform.Rotate(transform.rotation.x + rotation_xoffset, transform.rotation.y,transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        transform.position = Car.transform.position + offset;
    }
}
