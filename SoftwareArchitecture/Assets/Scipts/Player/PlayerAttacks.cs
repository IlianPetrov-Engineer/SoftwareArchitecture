using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] Ability[] abilities;
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask enemy;

    public int selectedAttack = 0;

    public bool canAttack = true;

    Dictionary<Ability.AbilityType, Ability> unlockedAbilities = new Dictionary<Ability.AbilityType, Ability>();

    Dictionary<Ability, float> lastCastTimes = new Dictionary<Ability, float>();

    private void Start()
    {
        if (abilities[0] != null)
        {
            unlockedAbilities[abilities[0].abilityType] = abilities[0];
        }
    }

    public bool LearnedAbilitiy(Ability.AbilityType type)
    {
        return unlockedAbilities.ContainsKey(type);
    }

    public void UnlockAbility(Ability unlockAbility)
    {
        if (LearnedAbilitiy(unlockAbility.abilityType))
            return;

        unlockedAbilities[unlockAbility.abilityType] = unlockAbility;

        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] == null)
            {
                abilities[i] = unlockAbility;
                return;
            }
        }
    }

    public void UpgradeAbility(Ability newLevelAbility)
    {
        if (!LearnedAbilitiy(newLevelAbility.abilityType))
            return;

        Ability previousAbility = unlockedAbilities[newLevelAbility.abilityType];

        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] == previousAbility)
            {
                abilities[i] = newLevelAbility;
                unlockedAbilities[newLevelAbility.abilityType] = newLevelAbility;
                return;
            }
        }
    }

    void OnCast()
    {
        if (!canAttack)
            return;

        if (selectedAttack >= abilities.Length)
            return;

        Ability ability = abilities[selectedAttack];
        if (ability == null)
            return;

        if (lastCastTimes.TryGetValue(ability, out float lastTime))
        {
            if (Time.time < lastTime + ability.cooldown)
                return;
        }

        Ability.AbilityData data = new Ability.AbilityData
        {
            player = transform,
            camera = playerCamera,
            enemy = enemy
        };

        ability.Cast(data);
        lastCastTimes[ability] = Time.time;
    }

    void OnFireballAttack()
    {
        selectedAttack = 0;
    }

    void OnFrostAttack()
    {
        selectedAttack = 1;
    }

    void OnGravityPushAttack()
    {
        selectedAttack = 2;
    }

    public Ability GetSelectedAbility()
    {
        if (selectedAttack >= abilities.Length)
            return null;

        return abilities[selectedAttack];
    }
}
