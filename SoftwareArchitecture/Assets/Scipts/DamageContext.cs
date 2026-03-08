using UnityEngine;
using static Attacks;

public struct DamageContext
{
    public DamageData damageData;
    public AttackType attackType;
    public Transform target;

    public DamageContext(DamageData damageData, AttackType attackType, Transform target)
    {
        this.damageData = damageData;
        this.attackType = attackType;
        this.target = target;
    }
}