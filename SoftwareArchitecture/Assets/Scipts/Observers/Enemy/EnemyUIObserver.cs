using SA_Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIObserver : EnemyObserver
{
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthNumber;

    protected override void OnEnemyCreated(Enemy enemy)
    {
        healthBar.maxValue = enemy.MaxHealth;
        healthBar.value = enemy.CurrentHealth;
        healthNumber.text = $"{enemy.CurrentHealth} / {enemy.MaxHealth}";
    }

    protected override void OnEnemyHit(Enemy enemy, DamageData damageData)
    {
        healthBar.value = enemy.CurrentHealth;
        healthNumber.text = $"{enemy.CurrentHealth} / {enemy.MaxHealth}";
    }

    protected override void OnEnemyDied(Enemy enemy) {}
}
