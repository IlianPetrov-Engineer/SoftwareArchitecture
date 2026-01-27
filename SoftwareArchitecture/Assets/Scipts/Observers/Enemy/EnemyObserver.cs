using UnityEngine;
using SA_Enemy;

/// <summary>
/// The abstract class for concrete enemy observers, the abstract class subscribes to onEnemyCreated
/// for initialization and onEnemyHit for getting enemy hit notification and acting accordingly
/// </summary>

public abstract class EnemyObserver : MonoBehaviour
{
    [SerializeField]
    protected EnemyController enemyController;

    protected void OnEnable()
    {
        enemyController.onEnemyCreated += OnEnemyCreated;
        enemyController.onHit += OnEnemyHit;
        enemyController.onEnemyDied += OnEnemyDied;
    }

    protected void OnDisable()
    {
        enemyController.onEnemyCreated -= OnEnemyCreated;
        enemyController.onHit -= OnEnemyHit;
        enemyController.onEnemyDied -= OnEnemyDied;
    }

    protected abstract void OnEnemyCreated(Enemy enemy);

    protected abstract void OnEnemyHit(Enemy enemy, DamageData damageData);

    protected abstract void OnEnemyDied(Enemy enemy);
}
