using SA_Enemy;
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
    }

    private void OnDisable()
    {
        EnemyController.OnEnemyDied -= OnEnemyKilled;
    }

    private void Start()
    {
        ActivateCurrentQuest();
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        if (CurrentQuest == null)
            return;

        CurrentQuest.goal.OnEnemyKilled();
        onQuestProgressChanged?.Invoke(CurrentQuest);

        if (CurrentQuest.IsCompleted)
        {
            CompleteCurrentQuest();
        }
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
