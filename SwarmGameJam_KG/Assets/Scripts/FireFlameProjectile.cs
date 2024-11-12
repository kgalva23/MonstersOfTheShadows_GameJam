using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlameProjectile : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction = Vector3.up; // Default direction

    // Set the direction of the projectile
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;

        // Rotate the projectile to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Destroy projectile if it goes out of bounds
        if (Mathf.Abs(transform.position.x) > 25 || Mathf.Abs(transform.position.y) > 25)
        {
            Destroy(gameObject);
        }
    }
}
