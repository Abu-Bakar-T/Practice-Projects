using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DetectCollision : MonoBehaviour
{
    private GameManager gm;
    private void Start() 
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // this script will only be called when enemy script trigger colssion disables
        // the trigger for the object.
        // the enemy script trigger collision will only disable trigger when theres no
        // hunger left is animal.
        // setting 0 and hungry and 1 as no hungery/
        // as long as value is 1, it will be destroyed upon collision with food
       if (collision.gameObject.CompareTag("Enemy"))
        {
            gm.IncreaseScore();
            Destroy(gameObject);
            Destroy(collision.gameObject);
            //Debug.Log("Collision detected with enemy!");
       }
    }

}
