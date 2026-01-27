using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown;

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
