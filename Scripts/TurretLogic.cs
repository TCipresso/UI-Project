using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1.0f;
    private GameObject target = null;
    private float fireTimer = 0.0f;

    void Update()
    {
        if (target != null)
        {
            FaceTarget();
            fireTimer += Time.deltaTime;
            if (fireTimer >= 1.0f / fireRate)
            {
                FireProjectile();
                fireTimer = 0.0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (target == null && other.gameObject.CompareTag("Enemy"))
        {
            target = other.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (target == null && other.gameObject.CompareTag("Enemy"))
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            target = null;
        }
    }

    void FaceTarget()
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectileLogic projectileLogic = projectile.GetComponent<ProjectileLogic>();
        if (projectileLogic != null)
        {
            projectileLogic.SetTarget(target);
        }
    }
}
