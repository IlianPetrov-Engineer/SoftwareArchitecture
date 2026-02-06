using TMPro;
using UnityEngine;

public class QuestUIObserver : MonoBehaviour
{
    [SerializeField] QuestManager questManager;

    [SerializeField] TextMeshProUGUI tittle;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI progress;

    private void OnEnable()
    {
        questManager.onQuestActivated += UpdateUI;
        questManager.onQuestProgressChanged += UpdateUI;
    }

    private void OnDisable()
    {
        questManager.onQuestActivated -= UpdateUI;
        questManager.onQuestProgressChanged -= UpdateUI;
    }

    private void UpdateUI(Quest quest)
    {
        tittle.text = quest.title;
        description.text = quest.description;
        progress.text = $"{quest.goal.currentAmount}/{quest.goal.requiredAmount}";
    }
}
