using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] Attacks[] attacks;
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask enemy;

    public int selectedAttack = 0;

    public bool canAttack = true;

    Dictionary<Attacks.AttackType, Attacks> unlockedAttacks = new Dictionary<Attacks.AttackType, Attacks>();

    Dictionary<Attacks, float> lastCastTimes = new Dictionary<Attacks, float>();

    private void Start()
    {
        if (attacks[0] != null)
        {
            unlockedAttacks[attacks[0].attackType] = attacks[0];
        }
    }

    public bool LearnedAttack(Attacks.AttackType type)
    {
        return unlockedAttacks.ContainsKey(type);
    }

    public void UnlockAttack(Attacks unlockAbility)
    {
        if (LearnedAttack(unlockAbility.attackType))
            return;

        unlockedAttacks[unlockAbility.attackType] = unlockAbility;

        for (int i = 0; i < attacks.Length; i++)
        {
            if (attacks[i] == null)
            {
                attacks[i] = unlockAbility;
                return;
            }
        }
    }

    public void UpgradeAttack(Attacks newLevelAbility)
    {
        if (!LearnedAttack(newLevelAbility.attackType))
            return;

        Attacks previousAbility = unlockedAttacks[newLevelAbility.attackType];

        for (int i = 0; i < attacks.Length; i++)
        {
            if (attacks[i] == previousAbility)
            {
                attacks[i] = newLevelAbility;
                unlockedAttacks[newLevelAbility.attackType] = newLevelAbility;
                return;
            }
        }
    }

    void OnCast()
    {
        if (!canAttack)
            return;

        if (selectedAttack >= attacks.Length)
            return;

        Attacks ability = attacks[selectedAttack];
        if (ability == null)
            return;

        if (lastCastTimes.TryGetValue(ability, out float lastTime))
        {
            if (Time.time < lastTime + ability.cooldown)
                return;
        }

        Attacks.AttackData data = new Attacks.AttackData
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

    void OnSecondAttack()
    {
        if (attacks[1] != null)
        selectedAttack = 1;
    }

    void OnThirdAttack()
    {
        if (attacks[2] != null)
            selectedAttack = 2;
    }

    public Attacks GetSelectedAbility()
    {
        if (selectedAttack >= attacks.Length)
            return null;

        return attacks[selectedAttack];
    }
}
