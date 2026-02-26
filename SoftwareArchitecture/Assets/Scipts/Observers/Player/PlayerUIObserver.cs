using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIObserver : PlayerObserver
{
    [SerializeField] TextMeshProUGUI currentHP;
    [SerializeField] Slider healthSlider; 
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] Slider xpSlider;
    [SerializeField] TextMeshProUGUI currentXP;
    [SerializeField] TextMeshProUGUI xpPoints;

    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject allUI;


    protected override void OnPlayerAwake()
    {
        healthSlider.maxValue = playerStarts.maxHealth;
        healthSlider.value = playerStarts.currentHealth;
        currentHP.text = $"{playerStarts.currentHealth} / {playerStarts.maxHealth}";

        currentLevel.text = $"{"Current Level: "} {playerStarts.currentLevel}";

        xpSlider.maxValue = playerStarts.xpNeeded;
        xpSlider.value = playerStarts.currentXP;
        currentXP.text = $"{playerStarts.currentXP} / {playerStarts.xpNeeded}";

        xpPoints.text = $"{"Skill Points: " + playerStarts.skillTokens}";
        allUI.SetActive(true);
        deathUI.SetActive(false);
    }

    protected override void OnPlayerHealthChange()
    {
        healthSlider.value = playerStarts.currentHealth;
        currentHP.text = $"{playerStarts.currentHealth} / {playerStarts.maxHealth}";
    }

    protected override void OnXPGained()
    {
        currentLevel.text = $"{"Current Level: " } {playerStarts.currentLevel}";

        xpSlider.maxValue = playerStarts.xpNeeded;
        xpSlider.value = playerStarts.currentXP;
        currentXP.text = $"{playerStarts.currentXP} / {playerStarts.xpNeeded}";

        xpPoints.text = $"{"Skill Points: " + playerStarts.skillTokens}";
    }

    protected override void OnPlayerDied() 
    {
        deathUI.SetActive(true);
        allUI.SetActive(false);
    }
}
