using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] Ability[] abilities;
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask enemy;

    public int selectedAttack = 0;

    Dictionary<Ability, float> lastCastTimes = new Dictionary<Ability, float>();

    void OnCast()
    {
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
}
