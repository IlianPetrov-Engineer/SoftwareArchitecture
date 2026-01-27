using SA_Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIObserver : EnemyObserver
{
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject healthNumber;

    protected override void OnEnemyCreated(Enemy enemy)
    {
        healthBar.GetComponent<Slider>().maxValue = enemy.MaxHealth;
        healthBar.GetComponent<Slider>().value = enemy.MaxHealth;
        healthNumber.GetComponent<TextMeshProUGUI>().text = enemy.MaxHealth.ToString();
    }

    protected override void OnEnemyHit(Enemy enemy, DamageData damageData)
    {
        healthBar.GetComponent<Slider>().value = enemy.CurrentHealth;
        healthNumber.GetComponent<TextMeshProUGUI>().text = enemy.CurrentHealth.ToString();
    }

    protected override void OnEnemyDied(Enemy enemy) 
    {
        healthBar.SetActive(false);
    }
}
