using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform SpawnPoint;
    public Button StartGameButton;
    public float SurvivalTime = 300.0f;
    public float spawnRate = 1.0f;
    private float minSpawnRate = 0.1f;
    private float spawnRateDecay = 0.08f;
    private float decayInterval = 10.0f;
    private float timeSinceLastDecay = 0.0f;
    private float timeSinceStart = 0.0f;

    void Start()
    {
        StartGameButton.gameObject.SetActive(true);
        StartGameButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        StartGameButton.gameObject.SetActive(false);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (timeSinceStart < SurvivalTime)
        {
            Instantiate(EnemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
            yield return new WaitForSeconds(spawnRate);

            timeSinceStart += spawnRate;
            UpdateSpawnRate();
        }
    }

    void UpdateSpawnRate()
    {
        timeSinceLastDecay += spawnRate;
        if (timeSinceLastDecay >= decayInterval)
        {
            spawnRate = Mathf.Max(minSpawnRate, spawnRate - spawnRateDecay);
            timeSinceLastDecay = 0.0f;
        }
    }

    public void EnemyDestroyed()
    {
        // Add any logic needed for when an enemy is destroyed
    }
}
