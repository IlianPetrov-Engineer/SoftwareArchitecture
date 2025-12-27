using UnityEngine;

namespace SA_Enemy
{
    public class EnemyPrefabs:MonoBehaviour
    {
        public GameObject[] enemyPrefabs;

        public GameObject GetEnemyPrefab()
        {
            int r = Random.Range(0, enemyPrefabs.Length);
            return enemyPrefabs[r];
        }
    }
}