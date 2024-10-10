using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float startTurnSpeed = 20000f;
    [SerializeField] float horsePower;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] float rpm;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheel;
    [SerializeField] int wheelsOnGround;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        turnSpeed = startTurnSpeed;
        speed = 0;
    }

    /*
     
        Lower the center of mass: You can lower the center of mass of the car's Rigidbody using a script. You can also apply a "down pressure" force that depends on the car's velocity.
        Increase gravity: You can try increasing the gravity on the Rigidbody.
        Add a box collider: You can try adding a box collider to the car model.
        Edit the 3D model: You can try editing the 3D model so that the front of the car faces the +Z direction.
        Rotate the 3D model: You can try putting the 3D model inside a child object in the car's hierarchy, then rotating it -90 degrees around the Y axis.

     */
    // Fixed Update is good for Physics and Movement
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            if (horizontalInput == 0)
                turnSpeed = startTurnSpeed;
            else
                turnSpeed += 10f;

            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
            playerRb.AddRelativeTorque(Vector3.up * horizontalInput * turnSpeed);

            // Calculating Speed for Speedometer
            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); // This is for mph. 3.6 fot kmh
            speedometerText.SetText("Speed: " + speed + " mph");

            // Calculuting RPM for RPM Meter
            rpm = Mathf.RoundToInt((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }

        // Ending Game if Reached End
        if (transform.position.z > 418)
        {
            SceneManager.LoadScene("GameOver");
        }
    }


    // Count of our Wheel on Ground
    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach(WheelCollider wheel in allWheel)
        {
            if(wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if(wheelsOnGround == 4 || wheelsOnGround == 2)
        {
            return true;
        }
        else
        { 
            return false; 
        }
    }
}
