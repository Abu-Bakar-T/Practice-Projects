using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPreExistingCarsRandomly : MonoBehaviour
{
    public GameObject[] carsPrefab;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }

        // Now replacing the cars that i placed in game with random car prefabs to make the game a bit random
        for(int i = 0; i < childObjects.Length; i++)
        {
            //Instantiating the Replacement Car in same place as our object
            GameObject replacementObject = Instantiate(carsPrefab[Random.Range(0, carsPrefab.Length)], childObjects[i].transform.position, childObjects[i].transform.rotation);
            
            // setting the parent
            if(parent.transform != null)
            {
                replacementObject.transform.SetParent(parent.transform);
            }
            GameObject temp = childObjects[i];
            childObjects[i] = replacementObject;
            Destroy(temp);
        }
    }
}
