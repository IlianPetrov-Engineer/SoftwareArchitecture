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
    public int skillTokens = 0;

    [SerializeField] List<int> xpLevelData = new List<int>();

    public event Action OnLevelUp;
    public event Action OnXPChanged;
    public event Action OnHealthChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        OnXPChanged?.Invoke();

        if (currentLevel < xpLevelData.Count)
        {
            int xpNeeded = xpLevelData[currentLevel];

            if (currentXP >= xpNeeded)
            {
                LevelUp();
            }
        }
    }

    void LevelUp()
    {
        currentXP = 0;
        currentLevel++;
        skillTokens++;

        OnLevelUp?.Invoke();
        OnXPChanged?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        OnHealthChanged?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        //OnHealthChanged?.Invoke();
    }

    // ================= Checkpoint =================

    //public void ApplySnapshot(PlayerStatsSnapshot snapshot)
    //{
    //    MaxHealth = snapshot.maxHealth;
    //    CurrentHealth = snapshot.currentHealth;
    //    CurrentLevel = snapshot.currentLevel;
    //    CurrentXP = snapshot.currentXP;
    //    SkillTokens = snapshot.skillTokens;

    //    OnHealthChanged?.Invoke();
    //    OnXPChanged?.Invoke();
    //}
}
