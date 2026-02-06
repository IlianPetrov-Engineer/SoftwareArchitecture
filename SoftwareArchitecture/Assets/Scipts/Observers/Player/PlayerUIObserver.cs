using TMPro;
using UnityEngine;

public class PlayerUIObserver : PlayerObserver
{
    [SerializeField] TextMeshProUGUI maxHP;
    [SerializeField] TextMeshProUGUI currentHP;
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] TextMeshProUGUI requiredXP;

    protected override void OnPlayerAwake()
    {
        maxHP.text = playerStarts.maxHealth.ToString();
        currentHP.text = playerStarts.currentHealth.ToString();
        currentLevel.text = playerStarts.currentLevel.ToString();
        requiredXP.text = playerStarts.xpNeeded.ToString();
    }

    protected override void OnPlayerHealthChange()
    {
        currentHP.text = playerStarts.currentHealth.ToString();
    }

    protected override void OnXPGained()
    {
        currentLevel.text = playerStarts.currentLevel.ToString();
        requiredXP.text = playerStarts.xpNeeded.ToString();
    }

    protected override void OnPlayerDied() {}
}
