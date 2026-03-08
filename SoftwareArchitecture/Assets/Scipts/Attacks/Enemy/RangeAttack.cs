using SA_Enemy;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttack/RangeAttack")]
public class RangeAttack : EnemyAttack
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    
    public  override void BeginAttack(EnemyController enemyController, Transform target)
    {
        float offset = 2;

        if (enemyController.EnemyData.enemyBehaviour == EnemyData.EnemyBehaviour.Boss)
            offset = 3.5f;

        Vector3 direction = enemyController.transform.forward;
        Vector3 spawnPos = new Vector3(enemyController.transform.position.x, enemyController.transform.position.y + offset / 2, enemyController.transform.position.z) + direction * offset;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));
        projectile.GetComponent<Rigidbody>().linearVelocity = direction * speed;

        EnemyProjectileController projectileController = projectile.GetComponent<EnemyProjectileController>();
        projectileController.Initialise(enemyController.EnemyData.attackDamage, lifeTime);
    }
}
