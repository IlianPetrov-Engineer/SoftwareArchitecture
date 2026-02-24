using UnityEngine;

[CreateAssetMenu(fileName = "DamageColourConfigurator", menuName = "Visuals/DamageColourConfigurator")]
public class AttackColourConfigurator : ScriptableObject
{
    [Header("Enemy Colours")]
    public Color32 fireballAttack = new Color32(128, 0, 0, 128);
    public Color32 freezeAttack = new Color32(0, 0, 0, 128);
    public Color32 gravityAttack = new Color32(0, 128, 0, 128);
}
