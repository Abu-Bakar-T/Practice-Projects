using UnityEngine;
using System.Collections;
using TMPro;
using System; // Required for Type handling

public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI collectibleText; // Reference to the TextMeshProUGUI component
    public ParticleSystem correctEffect; // Reference to the ParticleSystem
    public AudioSource correctSound; // Reference to the Audio Source
    private bool collected = false; // Whether all collectibles are collected
    private bool effectsPlayed = false; // Ensure effects are only played once

    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();
        if (collectibleText == null)
        {
            Debug.LogError("UpdateCollectibleCount script requires a TextMeshProUGUI component on the same GameObject.");
            return;
        }
        UpdateCollectibleDisplay(); // Initial update on start
    }

    void Update()
    {
        UpdateCollectibleDisplay();
        if (collected && !effectsPlayed)
        {
            PlayEffects(); // Play effects only once
            effectsPlayed = true; // Prevent multiple plays
        }
    }

    private void PlayEffects()
    {
        if (correctEffect != null)
        {
            correctEffect.Play(); // Play the particle effect
        }

        if (correctSound != null)
        {
            correctSound.Play(); // Play the sound effect
        }
    }

    private void UpdateCollectibleDisplay()
    {
        int totalCollectibles = 0;

        // Check and count objects of type Collectible
        Type collectibleType = Type.GetType("Collectible");
        if (collectibleType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectibleType, FindObjectsSortMode.None).Length;
        }

        // Optionally, check and count objects of type Collectible2D
        Type collectible2DType = Type.GetType("Collectible2D");
        if (collectible2DType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectible2DType, FindObjectsSortMode.None).Length;
        }

        // Set collected to true if no collectibles remain
        collected = (totalCollectibles == 0);

        // Update the collectible count display
        collectibleText.text = $"Collectibles remaining: {totalCollectibles}";
    }
}
