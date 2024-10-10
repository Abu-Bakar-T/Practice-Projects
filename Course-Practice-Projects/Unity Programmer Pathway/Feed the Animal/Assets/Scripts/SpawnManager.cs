using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemy;
    private float zRange = 30f;
    private float xRange = 22f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemy" , 2 ,2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-xRange, xRange), 0f, zRange);
        int index = Random.Range(0, enemy.Length);
        Instantiate(enemy[index], spawnPosition, enemy[index].transform.rotation);

        if (Random.Range(0, 7) == 3)
        {
            Vector3 leftSpawnPosition = new Vector3(-xRange, 0f, Random.Range(0f, 20));
            Instantiate(enemy[index], leftSpawnPosition, Quaternion.Euler(0f, 90f, 0f));
        }
        else
        {
            Vector3 rightSpawnPosition = new Vector3(xRange, 0f, Random.Range(0f, 20));
            Instantiate(enemy[index], rightSpawnPosition, Quaternion.Euler(0f, -90f, 0f));
        }
    }
}
