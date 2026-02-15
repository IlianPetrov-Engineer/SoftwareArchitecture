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
    [SerializeField] EnemyData enemyData;
    private Enemy enemy;
    private EnemyNavMeshController navMeshController;

    [SerializeField] EnemyAttack enemyAttack;
    public EnemyAttack Attack => enemyAttack;

    public EnemyData EnemyData => enemyData;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy, DamageData> onHit;
    public event Action<Enemy> onEnemyDied;

    public static event Action<EnemyController> OnEnemyDied;

    void Start()
    {
        navMeshController = GetComponent<EnemyNavMeshController>();
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
            OnEnemyDied?.Invoke(this);
            Destroy(gameObject);
        }

        onHit?.Invoke(enemy, damageData);
    }

    public void ApplyFreeze(float slowness, float duration)
    {
        navMeshController?.ApplyFreeze(slowness, duration);
    }

    public void ApplyForce(Vector3 force,float maxDistance)
    {
        navMeshController?.ApplyForce(force, maxDistance);
    }
}