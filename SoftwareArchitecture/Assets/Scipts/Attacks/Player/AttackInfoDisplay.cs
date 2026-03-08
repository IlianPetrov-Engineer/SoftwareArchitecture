using TMPro;
using UnityEngine;

public class AttackInfoDisplay : MonoBehaviour
{
    public static string attackInfo;

    [SerializeField] TextMeshProUGUI attackInfoText;

    private void Update()
    {
        attackInfoText.text = attackInfo;
    }
}
