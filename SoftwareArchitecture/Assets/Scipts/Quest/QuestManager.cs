using SA_Enemy;
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

    private int currentQuestIndex = 0;

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

    private void CompleteCurrentQuest()
    {
        // reward
        // playerStats.AddXP(CurrentQuest.expReward);

        onQuestCompleted?.Invoke(CurrentQuest);

        currentQuestIndex++;
        ActivateCurrentQuest();
    }
}
