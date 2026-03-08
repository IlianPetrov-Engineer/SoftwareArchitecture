using UnityEngine;

public abstract class Attacks : ScriptableObject
{
    public string abilityName;
    public float cooldown;

    public AttackType attackType;
    public int requiredTokens;

    [TextArea]
    public string abilityDescription;

    public enum AttackType
    {
        Fireball,
        Freeze,
        GravityPush
    }
    

    public void Cast(AttackData data)
    {
        ExecuteAttack(data);
    }

    protected abstract void ExecuteAttack(AttackData data);

    public struct AttackData
    {
        public Transform player;
        public Transform camera;
        public LayerMask enemy;
    }
}
