using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5.0f; // Speed of the projectiles

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            FireBurst();
        }
    }

    void FireBurst()
    {
        // Directions: N, NE, E, SE, S, SW, W, NW
        Vector2[] directions = {
            Vector2.up,
            (Vector2.up + Vector2.right).normalized,
            Vector2.right,
            (Vector2.down + Vector2.right).normalized,
            Vector2.down,
            (Vector2.down + Vector2.left).normalized,
            Vector2.left,
            (Vector2.up + Vector2.left).normalized
        };

        foreach (Vector2 dir in directions)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;
        }
    }
}
