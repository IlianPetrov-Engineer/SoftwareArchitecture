using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] Attacks attackToGive;
    [SerializeField] PlayerAttacks playerAttacks;
    [SerializeField] Button button;
    [SerializeField] GameObject completionOverlay;

    public void OnPress()
    {
        PlayerStats stats = PlayerStats.Instance;

        if (stats.skillTokens < attackToGive.requiredTokens)
            return;

        if (!playerAttacks.LearnedAttack(attackToGive.attackType))
            playerAttacks.UnlockAttack(attackToGive);
        else
            playerAttacks.UpgradeAttack(attackToGive);

        stats.skillTokens -= attackToGive.requiredTokens;

        button.interactable = false;
        completionOverlay.SetActive(true);
    }
}
