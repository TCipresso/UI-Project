using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottgunProjectile : MonoBehaviour
{
    public float speed = 5.0f;
    public int damage = 20;
    private Vector3 direction;

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            direction = (newTarget.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHp enemyHealth = collision.gameObject.GetComponent<EnemyHp>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject, 2.5f);
        }
    }
}
