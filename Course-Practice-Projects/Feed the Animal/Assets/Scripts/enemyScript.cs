using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class enemyScript : MonoBehaviour
{
    public float speed=20f;
    public HungerBar hungerBarForegroundSprite;
    [SerializeField] float lowerBound = -20f;
    [SerializeField] GameManager gm;
    [SerializeField] Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        hungerBarForegroundSprite = GetComponentInChildren<HungerBar>();
        gm = FindObjectOfType<GameManager>();
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isGameActive)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (transform.position.z < lowerBound || transform.position.x < -25 || transform.position.x > 25)
            {
                gm.DecreaseLives();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("food"))
        {
            hungerBarForegroundSprite.UpdateHungerBar(1, 1);
            gm.IncreaseScore();
            other.gameObject.SetActive(false);

            // the below area disables trigger so that object could be destroyed after no hunger
            bool flag = hungerBarForegroundSprite.EnableTriggerCollision();
            if (flag)
            {
                if (myCollider != null)
                {
                    myCollider.isTrigger = false;
                }
            }
            //Debug.Log("Collision detected with enemy!");
        }
    }
}
