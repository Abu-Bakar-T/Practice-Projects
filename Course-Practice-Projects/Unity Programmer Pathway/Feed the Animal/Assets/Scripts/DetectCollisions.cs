using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    [SerializeField] GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            // destroy the animal
            gm.IncreaseScore();
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    void onCollisionEnter(Collider other)
    {
        
    }

}
