using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Freeze")]
public class FreezeAttack : Ability
{
    [SerializeField] float range;
    [SerializeField] float angle;
    [SerializeField] DamageData damageData;

    protected override void ExecuteAbility(AbilityData data)
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
                enemy.GetHit(damageData);
        }
    }
}
