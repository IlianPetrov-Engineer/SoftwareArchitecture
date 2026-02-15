using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttack/MeleeAttack")]
public class MeleeAttack : EnemyAttack
{
    public float range;

    public override void BeginAttack(EnemyController enemyController, Transform target)
    {
        if (Vector3.Distance(enemyController.transform.position, target.position) > range)
                return;

        PlayerStats.Instance.TakeDamage(enemyController.EnemyData.attackDamage);
    }
}
