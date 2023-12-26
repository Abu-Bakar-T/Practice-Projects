using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCar : MonoBehaviour
{
    public GameObject playerCar;
    private float movingCarSpeed = 20f;
    public ParticleSystem explosion;
    public AudioSource explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // for child comparison with player and apply operation on child.
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            // only destroy objects that are of child and
            // if its 50 z axis behind player car
            if (transform.GetChild(i) != null)
            {
                if (transform.GetChild(i).transform.position.z + 50 < playerCar.transform.position.z || transform.GetChild(i).transform.position.y < -3)
                {
                    explosionSound.Play();
                    explosion.transform.position = transform.GetChild(i).transform.position;
                    explosion.Play();
                    Destroy(transform.GetChild(i).gameObject);
                }
                else
                {
                    if (playerCar.transform.position.z + 35 > transform.GetChild(i).transform.position.z)
                    {
                        transform.GetChild(i).transform.Translate(0, 0, 1 * Time.deltaTime * movingCarSpeed);
                    }
                }
            }
        }
    }
}
