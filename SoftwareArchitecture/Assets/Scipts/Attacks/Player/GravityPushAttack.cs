using UnityEngine;

[CreateAssetMenu(menuName = "PlayerAttacks/GravityPush")]
public class GravityPushAttack : Ability
{
    public float radius;
    public float force;
    [SerializeField] DamageData damageData;

    protected override void ExecuteAbility(AbilityData data)
    {
        Collider[] hits = Physics.OverlapSphere(data.player.position, radius, data.enemy);

        foreach (Collider hit in hits)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();

            if (enemy != null)
            {
                var context = new DamageContext(damageData, abilityType, data.player);
                enemy.GetHit(context);

                Vector3 direction = (hit.transform.position - data.player.transform.position).normalized;
                enemy.ApplyForce(direction * force, radius);
            }
        }
    }
}
