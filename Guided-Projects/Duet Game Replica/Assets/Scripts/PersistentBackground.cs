using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentBackground : MonoBehaviour
{
    private static PersistentBackground instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }
}
