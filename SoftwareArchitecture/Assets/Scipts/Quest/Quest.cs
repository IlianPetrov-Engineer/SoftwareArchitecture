using UnityEngine;

[System.Serializable]
public class Quest
{
    public string title;
    public string description;

    public int xpReward;

    public QuestGoal goal;

    public bool IsCompleted => goal.isReached();
}
