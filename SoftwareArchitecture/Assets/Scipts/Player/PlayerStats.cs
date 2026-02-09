using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("Progression")]
    public int currentLevel = 0;
    public int currentXP = 0;
    public int xpNeeded = 0;
    public int skillTokens = 0;

    [SerializeField] List<int> xpLevelData = new List<int>();

    public event Action onPlayerAwake;
    public event Action onLevelUp;
    public event Action onXPChanged;
    public event Action onHealthChanged;
    public event Action onPlayerDeath;

    private void Awake()
    {
        xpNeeded = xpLevelData[currentLevel];
        Instance = this;
        onPlayerAwake?.Invoke();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentLevel < xpLevelData.Count)
        {
            if (currentXP >= xpNeeded)
            {
                LevelUp();
            }

            xpNeeded = xpLevelData[currentLevel];
        }

        onXPChanged?.Invoke();
    }

    void LevelUp()
    {
        currentXP = 0;
        currentLevel++;
        skillTokens++;

        onLevelUp?.Invoke();
        onXPChanged?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        onPlayerDeath?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        onHealthChanged?.Invoke();
    }
}
