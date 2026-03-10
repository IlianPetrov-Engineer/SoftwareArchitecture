using UnityEngine;

public class PlayerMisc : MonoBehaviour
{
    private PlayerStats playerStats;
    private QuestManager questManager;
    [SerializeField] GameObject wizardStaff;
    [SerializeField] GameObject wallBeforeBoss;
    [SerializeField] GameObject enemyBoss;

    private void Start()
    {
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        questManager = GameObject.FindFirstObjectByType<QuestManager>();
    }

    private void FixedUpdate()
    {
        if (/*questManager.CurrentQuest.title == "Boss Battle" && */questManager.currentQuestIndex == 2)
        {
            if (questManager.CurrentQuest.goal.currentAmount == 1)
                return;

            else if (questManager.CurrentQuest.goal.currentAmount == 0)
            {
                if (enemyBoss == null)
                    return;

                wallBeforeBoss.SetActive(false);
                enemyBoss.SetActive(true);

                if (wizardStaff != null)
                    return;
            }
        }

        if (questManager.currentQuestIndex == 1)
            wizardStaff.SetActive(true);
    }




#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.L))
                playerStats.skillTokens += 5;

            if (Input.GetKeyDown(KeyCode.Q))
                questManager.CompleteCurrentQuest();
        }
    }

#endif
}
