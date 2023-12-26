using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float startSpeed = 10f;
    public float startTurnSpeed = 30f;
    private float speed;
    private float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        turnSpeed = startTurnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput == 0)
            speed = startSpeed;
        else
            speed = speed + 0.1f;
        if (horizontalInput == 0)
            turnSpeed = startTurnSpeed;
        else
            turnSpeed = turnSpeed + 0.1f;

        transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * turnSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);

        if(transform.position.z > 418)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
