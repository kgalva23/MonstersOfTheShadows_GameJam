using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemiesManager : MonoBehaviour
{
    public float slowDuration = 5f;    // Duration of the slow effect
    public float slowFactor = 0.2f;    // Fraction of original speed (e.g., 0.3 = 30% of original speed)

    // Method to start the slow effect
    public void StartSlowEffect()
    {
        StartCoroutine(SlowAllEnemies());
    }

    IEnumerator SlowAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Slow down all enemies
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; // Skip if the enemy has been destroyed

            if (enemy.TryGetComponent<EnemyController>(out var enemyController))
            {
                enemyController.speed *= slowFactor;
            }
            else if (enemy.TryGetComponent<ShadowMonsterController>(out var shadowMonster))
            {
                shadowMonster.speed *= slowFactor;
            }
            else if (enemy.TryGetComponent<ShadowOctopusController>(out var shadowOctopus))
            {
                shadowOctopus.speed *= slowFactor;
            }
        }

        // Wait for the slow duration
        yield return new WaitForSeconds(slowDuration);

        // Restore original speeds
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; // Skip if the enemy has been destroyed

            if (enemy.TryGetComponent<EnemyController>(out var enemyController))
            {
                enemyController.speed /= slowFactor;
            }
            else if (enemy.TryGetComponent<ShadowMonsterController>(out var shadowMonster))
            {
                shadowMonster.speed /= slowFactor;
            }
            else if (enemy.TryGetComponent<ShadowOctopusController>(out var shadowOctopus))
            {
                shadowOctopus.speed /= slowFactor;
            }
        }
    }
}
