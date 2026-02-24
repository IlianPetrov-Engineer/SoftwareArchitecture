using UnityEngine;
using static Ability;

public struct DamageContext
{
    public DamageData damageData;
    public AbilityType abilityType;
    public Transform target;

    public DamageContext(DamageData damageData, AbilityType abilityType, Transform target)
    {
        this.damageData = damageData;
        this.abilityType = abilityType;
        this.target = target;
    }
}