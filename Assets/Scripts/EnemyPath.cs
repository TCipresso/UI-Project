using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public float speed = 5f;
    private List<GameObject> path;
    private int pathIndex = 0;
    public int PlayerDamage = 10;

    void Start()
    {
        path = GameObject.FindObjectOfType<GridManager>().PathCells;
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while (pathIndex < path.Count)
        {
            Vector3 start = transform.position;
            Vector3 end = path[pathIndex].transform.position;
            float pathTime = 0;

            while (pathTime < 1f)
            {
                pathTime += Time.deltaTime * speed / Vector3.Distance(start, end);
                transform.position = Vector3.Lerp(start, end, pathTime);
                yield return null;
            }

            pathIndex++;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("End"))
        {
            PlayerHp healthManager = FindObjectOfType<PlayerHp>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(PlayerDamage);
            }

            EnemyHp enemyHp = GetComponent<EnemyHp>();
            if (enemyHp != null)
            {
                enemyHp.Die();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
