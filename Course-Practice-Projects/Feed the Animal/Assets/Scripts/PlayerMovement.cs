using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    private float speed = 15.0f;
    private float xRange = 22f;
    private float zRange = -5f;
    private float zbRange = 20f;
    public GameObject[] food;
    private GameManager gm;
private void Start() {
    gm = FindObjectOfType<GameManager>();
}
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z > zbRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zbRange);
        }
        if (transform.position.z < zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int index = Random.Range(0, food.Length);
            Vector3 spawnPosition = transform.position;
            spawnPosition.z = spawnPosition.z + 1f;
            spawnPosition.y = spawnPosition.y + 1f;
            Instantiate(food[index], spawnPosition, food[index].transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Collision detected with enemy!");
        }
    }
 
}
