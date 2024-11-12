using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostCollectible : MonoBehaviour
{
    //[SerializeField] AudioSource lightningAudioSource;

    [SerializeField] LightningAudioController lightningAudioController; // Reference to the audio controller

    public float speedBoostAmount = 5f;  // Amount of speed to add
    public float boostDuration = 5f;     // Duration of the speed boost in seconds

    void Start()
    {
        // Find the lightningAudioController in the scene
        lightningAudioController = GameObject.FindObjectOfType<LightningAudioController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collects the speed boost
        if (other.CompareTag("Player"))
        {

            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Apply speed boost to the player
                playerController.ApplySpeedBoost(speedBoostAmount, boostDuration);
            }

            // Play the sound effect from the separate audio controller
            if (lightningAudioController != null)
            {
                lightningAudioController.PlaySound();
            }

            // Destroy the speed boost collectible after it has been collected
            Destroy(gameObject);
        }
    }
}
