using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown;

    public AbilityType abilityType;
    public int requiredTokens;

    [TextArea]
    public string abilityDescription;

    public enum AbilityType
    {
        Fireball,
        Freeze,
        GravityPush
    }
    

    public void Cast(AbilityData data)
    {
        ExecuteAbility(data);
    }

    protected abstract void ExecuteAbility(AbilityData data);

    public struct AbilityData
    {
        public Transform player;
        public Transform camera;
        public LayerMask enemy;
    }
}
