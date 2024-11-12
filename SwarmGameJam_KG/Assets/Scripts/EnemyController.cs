using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;

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
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.TakeDamage(10);
            //Destroy(gameObject);
        }
        
        if (other.CompareTag("Projectile"))
        {
            GameManager.instance.AddScore(10); // Add score when an enemy is destroyed
            GameManager.instance.EnemyDestroyed();       // Notify GameManager that an enemy was destroyed
            Destroy(gameObject);
        }
    }
}