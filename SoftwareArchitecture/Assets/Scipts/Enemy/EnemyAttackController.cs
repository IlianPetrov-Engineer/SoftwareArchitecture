using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    
    private Dictionary<EnemyAttack, float> lastCastTime = new Dictionary<EnemyAttack, float>();

    public bool CanAttack(Transform target)
    {
        EnemyAttack attack = enemyController.Attack;

        if (attack == null)
            return false;

        if (lastCastTime.TryGetValue(attack, out float lastTime))
        {
            if (Time.time < lastTime + attack.cooldown)
                return false;
        }

        attack.BeginAttack(enemyController, target);

        lastCastTime[attack] = Time.time;
        return true;
    }
}
