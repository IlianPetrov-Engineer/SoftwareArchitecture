using UnityEngine;

[CreateAssetMenu(menuName = "PlayerAttacks/Freeze")]
public class FreezeAttack : Ability
{
    [SerializeField] float range;
    [SerializeField] float angle;
    public DamageData damageData;

    protected override void ExecuteAbility(AbilityData abilityData)
    {
        Collider[] hits = Physics.OverlapSphere(abilityData.player.position, range, abilityData.enemy);

        foreach (Collider hit in hits)
        {
            Vector3 direction = (hit.transform.position - abilityData.player.transform.position).normalized;
            float dotAngle = Vector3.Angle(abilityData.player.forward, direction);

            if (dotAngle > angle * 0.5f) //enemy is ourside the attack
                continue;

            if (Physics.Raycast(abilityData.camera.position, direction, out RaycastHit ray, range))
            {
                if (ray.collider != hit)
                    continue;
            }

            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.GetHit(damageData);

                enemy.ApplyFreeze(damageData.slowDown, damageData.slowDownTime);
            }
        }
    }
}
