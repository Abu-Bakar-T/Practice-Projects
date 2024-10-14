using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
    public GameObject onCollectEffect;
    public AudioSource onCollectAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the VFX
            Instantiate(onCollectEffect, transform.position, transform.rotation);
            onCollectAudioSource.Play();
        }
    }
}
