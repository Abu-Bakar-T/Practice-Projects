using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private bool spawnObject;

    private void Start()
    {
        spawnObject = false;
        InvokeRepeating("AllowSpawnDog", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && spawnObject)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            spawnObject = false;
        }
    }

    void AllowSpawnDog()
    {
        spawnObject = true;
    }
}
