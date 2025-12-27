using UnityEngine;
using System.Collections;
using SA_Enemy;
using System.Collections.Generic;

/// <summary>
/// A very basic enemy spawner that just randomly picks a prefab in a wave
/// and spawn it
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 2f;

    EnemyPrefabs enemyPrefabs;
   
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
        enemyPrefabs = GetComponent<EnemyPrefabs>();
    }

    private IEnumerator SpawnCoroutine()
    {
        float timePassed = 0f;
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            timePassed += Time.time;

            if(timePassed >= spawnInterval)
            {
                GameObject enemyPrefab = enemyPrefabs.GetEnemyPrefab();
                Instantiate(enemyPrefab, Spawner(),Quaternion.identity);
                timePassed = 0f;
            }
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

