using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FreezeEnemiesCollectible : MonoBehaviour
{
    [SerializeField] FreezeEnemiesManager freezeManager;
    [SerializeField] FreezeAudioController freezeAudioController; // Reference to the audio controller

    void Start()
    {
        // Find the FreezeEnemiesManager & freezeAudioController in the scene
        freezeManager = GameObject.FindObjectOfType<FreezeEnemiesManager>();
        freezeAudioController = GameObject.FindObjectOfType<FreezeAudioController>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && freezeManager != null)
        {

            freezeManager.StartSlowEffect(); // Trigger the slow effect

            // Play the sound effect from the separate audio controller
            if (freezeAudioController != null)
            {
                freezeAudioController.PlaySound();
            }

            Destroy(gameObject); // Destroy the IceCube collectible after it’s used
        }
    }
}
