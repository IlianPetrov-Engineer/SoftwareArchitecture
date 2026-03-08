using SA_Enemy;
using System.Collections;
using UnityEngine;

public class EnemyAppearanceObserver : EnemyObserver
{
    [SerializeField] Renderer enemyBody;
    [SerializeField] Animator animator;
    [SerializeField] EnemyFSM enemyFSM;
    [SerializeField] AttackColourConfigurator colourConfigurator;

    protected override void OnEnemyCreated(Enemy enemy) {}

    protected override void OnEnemyHit(Enemy enemy, DamageContext context)
    {
        StartCoroutine(Flash(context));
    }

    protected override void OnEnemyDied(Enemy enemy)
    {
        StartCoroutine(PlayDeathAndDestroy());
    }

    private IEnumerator Flash(DamageContext context)
    {
        enemyFSM.animationLock = true;
        animator.Play("Get Hit");

        Color colour = GetColour(context.attackType);

        Color original = enemyBody.material.color;
        enemyBody.material.color = colour;

        yield return new WaitForSeconds(context.damageData.duration);

        enemyBody.material.color = original;
        enemyFSM.animationLock = false;
    }

    private IEnumerator PlayDeathAndDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        enemyFSM.animationLock = true;
        animator.Play("Dies");

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
            && !animator.IsInTransition(0));

        enemyFSM.animationLock = false;

        Destroy(gameObject);
    }

    private Color GetColour(Attacks.AttackType abilityType)
    {
        switch (abilityType)
        {
            case Attacks.AttackType.Fireball:
                return colourConfigurator.fireballAttack;

            case Attacks.AttackType.Freeze:
                return colourConfigurator.freezeAttack;

            case Attacks.AttackType.GravityPush:
                return colourConfigurator.gravityAttack;

            default:
                return Color.red;
        }
    }
}
