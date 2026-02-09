using Unity.VisualScripting;
using UnityEngine;

public class SkillTreePresenter : AbilityPresenter
{
    [SerializeField] Ability refAbility;

    public override void Presenter(Ability ability)
    {
        refAbility = ability;
    }

    private void Start()
    {
        Presenter(refAbility);
    }

    public void DisplayInfo()
    {
        if (refAbility == null)
            return;

        AbilityInfoDisplay.abilityInfo = Description();
    }

    public void ClearInfo()
    {
        AbilityInfoDisplay.abilityInfo = "";
    }

    public string Description()
    {
        string text = refAbility.abilityName + "\n";
        text += refAbility.abilityDescription + "\n";
        text += "Cooldown: " + refAbility.cooldown + "\n";

        if (refAbility is FireballAttack fireball)
        {
            text += "Damage: " + fireball.damageData.damage;
        }

        else if (refAbility is FreezeAttack freeze)
        {
            text += "Slow: " + freeze.damageData.slowDown;
            text += "\nDuration: " + freeze.damageData.slowDownTime;
        }
        
        else if (refAbility is GravityPushAttack gravity)
        {
            text += "Push Force: " + gravity.force;
        }

        return text;
    }
}
