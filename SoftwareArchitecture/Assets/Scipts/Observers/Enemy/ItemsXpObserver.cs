using SA_Enemy;
using UnityEngine;

public class ItemsXpObserver : EnemyObserver
{
    [SerializeField] XP xpPrefab;
    [SerializeField] EnemyData enemyData;
    [SerializeField] DropableItems dropableItems;

    protected override void OnEnemyCreated(Enemy enemy) {}

    protected override void OnEnemyHit(Enemy enemy, DamageContext context) {}

    protected override void OnEnemyDied(Enemy enemy)
    {
        float offset = 2;
        Vector3 spawnPos = new Vector3(enemyController.transform.position.x, enemyController.transform.position.y + offset,
            enemyController.transform.position.z);

        if (xpPrefab != null)
        {
            XP xp = Instantiate(xpPrefab, spawnPos, Quaternion.identity);

            xp.xp = enemyData.xp;
        }

        if (dropableItems != null)
            dropableItems.Drop();
    }
}
