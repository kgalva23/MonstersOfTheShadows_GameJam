using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMonsterController : MonoBehaviour
{
    public float speed = 3f;

    public Transform player;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Move towards the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Flip the octopus based on the direction
            FlipShadowMonster(direction);
        }
    }

    // Flip the octopus based on direction
    void FlipShadowMonster(Vector3 direction)
    {
        if (direction.x > 0)
        {
            // Face right (set localScale.x to positive)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            // Face left (set localScale.x to negative)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(10);
        }
        
        if (other.CompareTag("Projectile"))
        {
            GameManager.instance.AddScore(20); // Add score when an enemy is destroyed
            GameManager.instance.EnemyDestroyed();       // Notify GameManager that an enemy was destroyed
            Destroy(gameObject);
        }
    }
}
