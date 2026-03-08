using UnityEngine;

public class SkillTreePresenter : AttacksPresenter
{
    [SerializeField] Attacks refAbility;

    public override void Presenter(Attacks attacks)
    {
        refAbility = attacks;
    }

    private void Start()
    {
        Presenter(refAbility);
    }

    public void DisplayInfo()
    {
        if (refAbility == null)
            return;

        AttackInfoDisplay.attackInfo = Description();
    }

    public void ClearInfo()
    {
        AttackInfoDisplay.attackInfo = "";
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
            text += "\nDuration: " + freeze.damageData.duration;
        }
        
        else if (refAbility is GravityPushAttack gravity)
        {
            text += "Push Force: " + gravity.force;
        }

        text += "\n" + "Required tokens: " + refAbility.requiredTokens;

        return text;
    }
}
