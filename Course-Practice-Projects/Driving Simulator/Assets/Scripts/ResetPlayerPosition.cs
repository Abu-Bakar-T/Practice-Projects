using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    public CarMovement carMovemet;
    [SerializeField] Rigidbody carRb;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || transform.position.y < -10)
        {
            carRb.angularVelocity = Vector3.zero;
            carRb.velocity = Vector3.zero;
            transform.position = new Vector3(-2.938f, 0.858874f, -71.95f);
            transform.rotation = Quaternion.Euler(-0.253f, -0.001f, 0f);
            carMovemet.horizontalInput = 0;
            carMovemet.verticalInput = 0;
        }
    }
}
