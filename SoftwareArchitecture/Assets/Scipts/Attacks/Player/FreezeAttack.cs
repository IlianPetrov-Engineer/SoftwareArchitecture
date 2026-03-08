using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerAttacks/Freeze")]
public class FreezeAttack : Attacks
{
    public float range;
    public float angle;
    public DamageData damageData;

    protected override void ExecuteAttack(AttackData data)
    {
        Collider[] hits = Physics.OverlapSphere(data.player.position, range, data.enemy);

        foreach (Collider hit in hits)
        {
            Vector3 direction = (hit.transform.position - data.player.transform.position).normalized;
            float dotAngle = Vector3.Angle(data.player.forward, direction);

            if (dotAngle > angle * 0.5f) //enemy is ourside the attack
                continue;

            if (Physics.Raycast(data.camera.position, direction, out RaycastHit ray, range))
            {
                if (ray.collider != hit)
                    continue;
            }

            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy != null)
            {
                var context = new DamageContext(damageData, attackType, data.player);

                enemy.GetHit(context);

                enemy.ApplyFreeze(damageData.slowDown, damageData.duration);
            }
        }
    }
}
