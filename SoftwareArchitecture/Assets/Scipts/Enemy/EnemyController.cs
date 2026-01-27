using UnityEngine;
using System;
using SA_Enemy;
using UnityEngine.UI;

/// <summary>
/// Simple enemy controller that publish onEnemyCreated and onHit events when
/// it's created and hit.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    private Enemy enemy;

    //[SerializeField] XP xp;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy, DamageData> onHit;
    public event Action<Enemy> onEnemyDied;

    void Start()
    {
        enemy = enemyData.CreateEnemy();
        onEnemyCreated?.Invoke(enemy);
    }

    public void GetHit(DamageData damageData)
    {
        if (enemy.CurrentHealth <= 0)
            return;

        enemy.CurrentHealth -= damageData.damage;

        if (enemy.CurrentHealth <= 0)
        {
            enemy.CurrentHealth = 0;
            onEnemyDied?.Invoke(enemy);
            Destroy(gameObject);
        }

        onHit?.Invoke(enemy, damageData);
    }
}