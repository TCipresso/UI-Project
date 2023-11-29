using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTurretLogic : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1.0f;
    public int pelletCount = 5; // Number of pellets per shot
    public float spreadAngle = 30.0f; // Spread angle in degrees
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
        Vector2 baseDirection = (target.transform.position - transform.position).normalized;
        for (int i = 0; i < pelletCount; i++)
        {
            GameObject pellet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            ShottgunProjectile projectileLogic = pellet.GetComponent<ShottgunProjectile>();

            if (projectileLogic != null)
            {
                // Calculate the spread for each pellet
                float spread = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                Vector2 spreadDirection = Quaternion.Euler(0, 0, spread) * baseDirection;

                GameObject fakeTarget = new GameObject("FakeTarget");
                fakeTarget.transform.position = transform.position + (Vector3)spreadDirection * 100; // Arbitrary distance
                projectileLogic.SetTarget(fakeTarget);

                Destroy(fakeTarget, 2.0f); // Destroy the fake target after a short delay
            }
        }
    }
}
