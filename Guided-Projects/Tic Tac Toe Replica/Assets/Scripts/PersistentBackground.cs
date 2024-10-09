using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentBackground : MonoBehaviour
{
    private static PersistentBackground instance;
    void Awake()
    {
        // Check if an instance of the background already exists
        if (instance != null)
        {
            // If so, destroy the new instance to prevent duplicates
            Destroy(gameObject);
        }
        else
        {
            // Set the current instance and prevent destruction
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
