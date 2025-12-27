using UnityEngine;
using System;
using SA_Enemy;

/// <summary>
/// Simple enemy controller that publish onEnemyCreated and onHit events when
/// it's created and hit.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;
    private Enemy enemy;

    [SerializeField] XP xp;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy, DamageData> onHit;

    void Start()
    {
        enemy = enemyData.CreateEnemy();
        onEnemyCreated?.Invoke(enemy);
    }

    public void GetHit(DamageData damageData)
    {
        enemy.CurrentHealth -= damageData.damage;
        if (enemy.CurrentHealth < 0)
        {
            enemy.CurrentHealth = 0;
        }

        Debug.Log("Current health:" + enemy.CurrentHealth);

        onHit?.Invoke(enemy, damageData);
    }

    public void XP()
    {
        xp.xp = enemyData.xp;
        Instantiate(xp, transform.position, Quaternion.identity);
    }
}




