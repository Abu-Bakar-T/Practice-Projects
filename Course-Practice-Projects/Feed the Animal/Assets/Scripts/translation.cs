using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translation : MonoBehaviour
{
    public float speed = 20f;
    [SerializeField] GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isGameActive)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
