using TMPro;
using UnityEngine;

public class AbilityInfoDisplay : MonoBehaviour
{
    public static string abilityInfo;

    [SerializeField] TextMeshProUGUI abilityInfoText;

    private void Update()
    {
        abilityInfoText.text = abilityInfo;
    }
}
