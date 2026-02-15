using UnityEngine;

public abstract class EnemyAttack : ScriptableObject
{
    public float cooldown;

    public abstract void BeginAttack(EnemyController enemyController, Transform target);
}
