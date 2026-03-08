using UnityEngine;
using System;
using SA_Enemy;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    private Enemy enemy;
    private EnemyNavMeshController navMeshController;

    [SerializeField] List<EnemyAttack> enemyAttack = new List<EnemyAttack>();
    public List<EnemyAttack> Attack => enemyAttack;

    public EnemyData EnemyData => enemyData;

    public event Action<Enemy> onEnemyCreated;
    public event Action<Enemy, DamageContext> onHit;
    public event Action<Enemy> onEnemyDied;

    public static event Action<EnemyController> OnEnemyDied;

    void Start()
    {
        navMeshController = GetComponent<EnemyNavMeshController>();
        enemy = enemyData.CreateEnemy();
        onEnemyCreated?.Invoke(enemy);
    }

    public void GetHit(DamageContext context)
    {
        if (enemy.CurrentHealth <= 0)
            return;

        enemy.CurrentHealth -= context.damageData.damage;

        onHit?.Invoke(enemy, context);

        if (enemy.CurrentHealth <= 0)
        {
            enemy.CurrentHealth = 0;
            onEnemyDied?.Invoke(enemy);
            OnEnemyDied?.Invoke(this);
        }
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