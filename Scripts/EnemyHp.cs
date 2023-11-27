using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int currencyValue = 10;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        RoundManager roundManager = FindObjectOfType<RoundManager>();
        if (roundManager != null)
        {
            roundManager.EnemyDestroyed();
        }
        EcoManager.Instance.AddCurrency(currencyValue);
        Destroy(gameObject);
    }
}
