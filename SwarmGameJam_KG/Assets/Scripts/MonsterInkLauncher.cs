using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInkLauncher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float projectileSpeed = 10.0f;

    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Helpers")]
    [SerializeField] Transform spawnTransform;

    [Header("Audio")]
    [SerializeField] AudioSource monsterInkAudioSource;
    [Range(-5, -1)]
    [SerializeField] float pitchRange = .2f;

    [Header("Timing")]
    [SerializeField] public float fireRate = 3.0f; // Time between shots in seconds

    public bool isFiring = false;  // Track if firing is active

    public Transform playerTransform;
    
    void Start()
    {
        // Find the player object by tag at the start
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player object has the 'Player' tag.");
        }
    }

    void FixedUpdate()
    {
        if (!isFiring) // Ensure the coroutine only starts once
        {
            StartCoroutine(FireProjectileAtInterval());
        }
    }

    // Coroutine to fire the projectile at regular intervals
    public IEnumerator FireProjectileAtInterval()
    {
        isFiring = true;  // Set firing to active

        while (true) // Infinite loop for continuous firing
        {
            yield return new WaitForSeconds(fireRate);  // Wait for the defined interval before firing again
            Launch();  // Fire a projectile
        }
    }

    // Launch a projectile directly toward the player
    public void Launch()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform is null. Cannot launch projectile.");
            return;
        }

        // Instantiate the projectile at the spawn position
        GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, Quaternion.identity);

        // Calculate the direction from the enemy to the player
        Vector2 direction = (playerTransform.position - spawnTransform.position).normalized;

        // Set the projectile's velocity towards the player
        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        // Flip the projectile if facing right
        if (direction.x > 0)
        {
            Vector3 projectileScale = newProjectile.transform.localScale;
            projectileScale.x = -Mathf.Abs(projectileScale.x); // Flip it to face left
            newProjectile.transform.localScale = projectileScale;
        }
        else
        {
            Vector3 projectileScale = newProjectile.transform.localScale;
            projectileScale.x = Mathf.Abs(projectileScale.x); // Ensure it’s facing right
            newProjectile.transform.localScale = projectileScale;
        }

        monsterInkAudioSource.pitch = Random.Range(1f-pitchRange,1f+pitchRange);
        monsterInkAudioSource.Play();

        // Destroy the projectile after 2 seconds to prevent memory leaks
        Destroy(newProjectile, 2);
    }
}

