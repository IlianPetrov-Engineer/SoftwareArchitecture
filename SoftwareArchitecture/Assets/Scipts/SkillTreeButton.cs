using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] Ability abilityToGive;
    [SerializeField] PlayerAttacks playerAttacks;
    [SerializeField] Button button;

    public void OnPress()
    {
        PlayerStats stats = PlayerStats.Instance;

        if (stats.skillTokens < abilityToGive.requiredTokens)
            return;

        if (!playerAttacks.LearnedAbilitiy(abilityToGive.abilityType))
            playerAttacks.UnlockAbility(abilityToGive);
        else
            playerAttacks.UpgradeAbility(abilityToGive);

        stats.skillTokens -= abilityToGive.requiredTokens;

        button.interactable = false;
    }
}
