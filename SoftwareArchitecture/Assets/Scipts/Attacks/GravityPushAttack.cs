using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/GravityPush")]
public class GravityPushAttack : Ability
{
    [SerializeField] float radius;
    [SerializeField] float force;

    protected override void ExecuteAbility(AbilityData data)
    {
        Collider[] hits = Physics.OverlapSphere(data.player.position, radius, data.enemy);

        foreach (Collider hit in hits)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy == null)
                continue;

            Vector3 direction = (hit.transform.position - data.player.transform.position).normalized;
            //enemy.ApplyForce(direction * force);
        }
    }
}
