using UnityEngine;
using System.Collections;
using SA_Enemy;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] int maxEnemyCount;
    private int currentCount;

    private bool isSpawning = true;

    public EnemyPrefabs enemyPrefabs;
   
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
        enemyPrefabs = GetComponent<EnemyPrefabs>();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentCount >= maxEnemyCount)
                yield break;

            GameObject enemyPrefab = enemyPrefabs.GetEnemyPrefab();
            Instantiate(enemyPrefab, Spawner(), Quaternion.identity);
            currentCount++;
        }
    }

    public Vector3 Spawner()
    {
        Bounds bounds = gameObject.GetComponent<Collider>().bounds;

        Vector3 randomPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
            );

        return randomPosition;
    }
}

