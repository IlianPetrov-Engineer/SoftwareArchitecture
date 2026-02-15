using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttack/AuraAttack")]
public class AuraAttack : EnemyAttack
{
    [SerializeField] float radius;
    [SerializeField] float tickRate;
   
    private Dictionary<EnemyController, float> lastTick = new Dictionary<EnemyController, float>();

    private void Awake()
    {
        if (cooldown > 0)
            cooldown = 0;
    }

    public override void BeginAttack(EnemyController enemyController, Transform target)
    {
        if(!lastTick.ContainsKey(enemyController))
            lastTick[enemyController] = 0;

        if (Time.time < lastTick[enemyController] +  tickRate) 
            return;

        if(Vector3.Distance(enemyController.transform.position, target.position) <= radius)
        {
            PlayerStats.Instance.TakeDamage(enemyController.EnemyData.passiveDamage);
            lastTick[enemyController] = Time.time;
        }
    }
}
