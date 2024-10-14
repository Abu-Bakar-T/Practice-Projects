using UnityEngine;
using System.Collections;

public class ballSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource woodSource;
    private float soundDuration = 2f; // Duration for playing sounds
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            audioSource.Play();
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            PlaySound(woodSource);
        }
    }

    private void PlaySound(AudioSource source)
    {
        if (source != null)
        {
            StartCoroutine(PlaySoundForDuration(source, soundDuration));
        }
    }

    private IEnumerator PlaySoundForDuration(AudioSource source, float duration)
    {
        if (!source.isPlaying)
        {
            source.Play(); // Play the sound
        }

        yield return new WaitForSeconds(duration); // Wait for the specified duration

        source.Stop(); // Stop the sound after duration
    }
}
