using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public Sprite downSprite;  // Sprite for down direction
    public Sprite upSprite;    // Sprite for up direction
    public Sprite leftSprite;  // Sprite for left direction
    public Sprite rightSprite; // Sprite for right direction

    public SpriteRenderer spriteRenderer;

    public float speed = 5f;          // Default speed
    public float originalSpeed;       // Store the original speed
    public float speedBoostTimer = 0f; // Track remaining boost time
    public bool isBoosted = false;    // Check if boost is active

    [SerializeField] AudioSource fireballAudioSource;
    [Range(0,1)]
    [SerializeField] float pitchRange = .2f;

    void Awake()
    {
        GameTimer.Instance.ResetTimer();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpeed = speed; // Initialize original speed
    }

    void Update()
    {
        // Shoot based on directional keys
        if (Input.GetKeyDown(KeyCode.I)) // Up
        {
            Shoot(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.J)) // Left
        {
            Shoot(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.K)) // Down
        {
            Shoot(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.L)) // Right
        {
            Shoot(Vector3.right);
        }
    }

    void FixedUpdate()
    {
        // Handle movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, moveY, 0f) * speed * Time.deltaTime;
        transform.position += movement;

        // Change sprite based on movement direction
        UpdateSpriteDirection(moveX, moveY);

        // Update boost timer
        if (isBoosted)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                ResetSpeed();
            }
        }
    }

    void UpdateSpriteDirection(float moveX, float moveY)
    {
        if (moveX > 0) // Moving right
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (moveX < 0) // Moving left
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (moveY > 0) // Moving up
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (moveY < 0) // Moving down
        {
            spriteRenderer.sprite = downSprite;
        }
    }

    void Shoot(Vector3 direction)
    {
        // Instantiate the projectile at the fire point
        GameObject fireball = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Set the direction for the projectile
        FireFlameProjectile projectileScript = fireball.GetComponent<FireFlameProjectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction); // Use the new SetDirection method
        }

        // Play the fireball sound with a random pitch variation
        fireballAudioSource.pitch = Random.Range(1f - pitchRange, 1f + pitchRange);
        fireballAudioSource.Play();
    }

    public void ApplySpeedBoost(float boostAmount, float duration)
    {
        if (!isBoosted)
        {
            speed += boostAmount;
            speedBoostTimer = duration;
            isBoosted = true;
        }
    }

    public void ResetSpeed()
    {
        speed = originalSpeed; // Reset to original speed
        isBoosted = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MonsterInk"))
        {
            GameManager.instance.TakeDamage(10);
        }
        else if (other.CompareTag("Ink"))
        {
            GameManager.instance.TakeDamage(20);
        }
        
    }
}
