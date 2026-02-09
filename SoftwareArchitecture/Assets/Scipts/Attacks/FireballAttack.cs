using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Fireball")]
public class FireballAttack : Ability
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed;
    public DamageData damageData;
    [SerializeField] float lifeTime;

    protected override void ExecuteAbility(AbilityData data)
    {
        float offset = 2;

        Vector3 spawnPos = data.camera.position + data.camera.forward * offset;
        Vector3 direction = data.camera.forward;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));
        projectile.GetComponent<Rigidbody>().linearVelocity = direction * speed;

        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.Initialise(damageData, lifeTime);
    }
}
