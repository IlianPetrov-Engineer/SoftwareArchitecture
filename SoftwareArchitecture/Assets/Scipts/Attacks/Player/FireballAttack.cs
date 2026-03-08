using UnityEngine;
using static Attacks;

[CreateAssetMenu(menuName = "PlayerAttacks/Fireball")]
public class FireballAttack : Attacks
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed;
    public DamageData damageData;
    [SerializeField] float lifeTime;

    protected override void ExecuteAttack(AttackData data)
    {
        float offset = 2;

        Vector3 spawnPos = data.camera.position + data.camera.forward * offset;
        Vector3 direction = data.camera.forward;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));
        projectile.GetComponent<Rigidbody>().linearVelocity = direction * speed;

        var context = new DamageContext(damageData, attackType, data.player);
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.Initialise(context, lifeTime);
    }
}
