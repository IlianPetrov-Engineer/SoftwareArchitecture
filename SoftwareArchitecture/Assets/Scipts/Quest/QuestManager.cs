using SA_Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> quests = new List<Quest>();

    public event Action<Quest> onQuestActivated;
    public event Action<Quest> onQuestCompleted;
    public event Action<Quest> onQuestProgressChanged;

    public int currentQuestIndex = 0;

    [SerializeField] PlayerStats playerStats;

    private void OnEnable()
    {
        EnemyController.OnEnemyDied += OnEnemyKilled;
        ItemCreation.onGetItem += OnItemGathered;
    }

    private void OnDisable()
    {
        EnemyController.OnEnemyDied -= OnEnemyKilled;
        ItemCreation.onGetItem -= OnItemGathered;
    }

    private void Start()
    {
        ActivateCurrentQuest();
    }

    private void OnEnemyKilled(EnemyController controller)
    {
        if (CurrentQuest == null)
            return;

        CurrentQuest.goal.OnEnemyKilled(controller);
        onQuestProgressChanged?.Invoke(CurrentQuest);

        if (CurrentQuest.IsCompleted)
            CompleteCurrentQuest();
    }

    private void OnItemGathered(Item item)
    {
        if (CurrentQuest == null) 
            return;

        CurrentQuest.goal.OnItemGathered(item);

        onQuestProgressChanged?.Invoke(CurrentQuest);

        if (CurrentQuest.IsCompleted)
            CompleteCurrentQuest();
    }

    public Quest CurrentQuest
    {
        get
        {
            if (currentQuestIndex < quests.Count)
            {
                return quests[currentQuestIndex];
            }

            return null;
        }
    }

    private void ActivateCurrentQuest()
    {
        if (CurrentQuest == null)
            return;

        CurrentQuest.goal.currentAmount = 0;

        onQuestActivated?.Invoke(CurrentQuest);
    }

    public void CompleteCurrentQuest()
    {
        playerStats.AddXP(CurrentQuest.xpReward);

        onQuestCompleted?.Invoke(CurrentQuest);

        currentQuestIndex++;
        ActivateCurrentQuest();
    }
}
