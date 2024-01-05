using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFunctionality : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Car;
    [SerializeField] bool carMovement = false;
    [SerializeField] float speed = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Car.transform.position.z + 30 > transform.position.z)
            carMovement = true;
        if (carMovement)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
      
    }
}
