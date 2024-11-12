using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    [SerializeField] HeartAudioController heartAudioController; // Reference to the audio controller

    public int healthIncrease = 20;     // Amount of health to add

    void Start()
    {
        // Find the FreezeEnemiesManager in the scene
        heartAudioController = GameObject.FindObjectOfType<HeartAudioController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collects the heart
        if (other.CompareTag("Player"))
        {

            Debug.Log("Health collected. +20 health");

            // Access the GameManager instance and update the player's health
            int currentHealth = GameManager.instance.health;

            // Increase health by 20, capping it at 100
            if (currentHealth >= 80)
            {
                GameManager.instance.health = 100;
            }
            else
            {
                GameManager.instance.health += healthIncrease;
            }

            // Update the UI or any health-related visuals in GameManager
            GameManager.instance.UpdateUI();

            // Play the sound effect from the separate audio controller
            if (heartAudioController != null)
            {
                heartAudioController.PlaySound();
            }

            // Destroy the heart collectible after it has been collected
            Destroy(gameObject);
        }
    }

}
