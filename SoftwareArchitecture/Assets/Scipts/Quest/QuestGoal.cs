using SA_Enemy;
using SA_Inventory;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public int requiredAmount;
    public int currentAmount;

    [SerializeField] ItemData itemData;
    [SerializeField] EnemyData enemyData;

    public GoalType goalType;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void OnEnemyKilled(EnemyController controller)
    {
        if (goalType != GoalType.Kill)
            return;

        if (enemyData == null)
            Debug.LogError("Kill quest is missing EnemyData reference");

        if (controller.EnemyData == enemyData)
        currentAmount++;
    }
    
    public void OnItemGathered(Item gatheredItem)
    {
        if(goalType != GoalType.Gather)
            return;

        if (itemData == null)
            Debug.LogError("Gather quest is missing ItemData reference");

        if(gatheredItem.Id == itemData.id)
        currentAmount++;
    }
}

public enum GoalType
{
    Kill,
    Gather
}
