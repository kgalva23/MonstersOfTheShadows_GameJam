using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text waveText;

    public int score = 0;
    public int health = 100;
    public int maxHealth = 100; // Define max health for the player
    public int wave = 1;
    public int activeEnemies = 0;
    public int enemiesToSpawn;

    public Image healthBar; // Add this reference

    [SerializeField] List<GameObject> spawningEnemies;
    [SerializeField] float spawnInterval = 1.0f;

    [SerializeField] RandomPowerupsSpawn powerupSpawner; // Reference to RandomPowerupsSpawn

    [SerializeField] GameStats gameStatsFinal;
    [SerializeField] GameObject gameOverPanel;  // Reference to the gameOverPanel

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
        StartNewWave();
    }

    void StartNewWave()
    {
        enemiesToSpawn = wave * 5; // Define the number of enemies for the current wave
        activeEnemies = 0;

        powerupSpawner.ResetPowerupCounter(); // Reset power-up counter at the start of each wave
        
        InvokeRepeating("SpawnEnemy", 0, spawnInterval); // Start spawning enemies
        UpdateUI();
    }

    void SpawnEnemy()
    {
        // Stop spawning if we have reached the target number of enemies for this wave
        if (enemiesToSpawn == 0)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }

        // Determine which enemies can spawn based on the wave number
        int maxEnemyIndex;
        if (wave <= 2)
        {
            maxEnemyIndex = 0; // Only Element 0
        }
        else if (wave <= 4)
        {
            maxEnemyIndex = 1; // Elements 0 and 1
        }
        else
        {
            maxEnemyIndex = 2; // Elements 0, 1, and 2
        }

        float randomPos = Random.Range(-16.0f, 16.0f);
        int randomIndex = Random.Range(0, maxEnemyIndex + 1); // Adjust range based on maxEnemyIndex

        Vector3 spawnPosition = new Vector3(randomPos, randomPos, 0f); // Spawn spread throughout map
        GameObject enemySpawn = Instantiate(spawningEnemies[randomIndex], spawnPosition, Quaternion.identity);
        enemySpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);

        activeEnemies++; // Increment active enemies after each spawn
        enemiesToSpawn--;
    }

    public void EnemyDestroyed()
    {
        activeEnemies--;

        // Start a new wave if all enemies are destroyed
        if (activeEnemies <= 0 && enemiesToSpawn == 0)
        {
            wave++;
            StartNewWave();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateUI();
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        waveText.text = "Wave: " + wave;

        // Update health bar fill based on health
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Final Score: " + score);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverPanel.SetActive(true);               // Set the WinPanel active to display it
        GameTimer.Instance.StopTimer();
        gameStatsFinal.DisplayGameStats();

        Time.timeScale = 0; // Pause all background objects
    }

    public int GetWave()
    {
        return wave;
    }

    public void destroyAllEnemies()
    {
        // Find all game objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Loop through each enemy and destroy it
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
