using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider hungerBar;

    private void Start()
    {
        hungerBar = GetComponent<Slider>();
    }

    public void UpdateHungerBar(float maxHunger, float currentHunger)
    {
        hungerBar.value = currentHunger / maxHunger;
    }

    public bool EnableTriggerCollision()
    {
        if(hungerBar.value == 1)
        {
            return true;
        }
        else
        { 
            return false; 
        }
    }
}