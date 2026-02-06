using SA_Enemy;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public int requiredAmount;
    public int currentAmount;

    public GoalType goalType;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void OnEnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }   
}

public enum GoalType
{
    Kill,
    Gather
}
