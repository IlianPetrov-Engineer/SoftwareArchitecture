using SA_Enemy;
using System.Collections;
using UnityEngine;

public class EnemyAppearanceObserver : EnemyObserver
{
    [SerializeField] Renderer enemyBody;
    [SerializeField] Animator animator;
    [SerializeField] EnemyFSM enemyFSM;

    protected override void OnEnemyCreated(Enemy enemy) {}

    protected override void OnEnemyHit(Enemy enemy, DamageData damageData)
    {
        StartCoroutine(FlashRed());
    }

    protected override void OnEnemyDied(Enemy enemy)
    {
        StartCoroutine(PlayDeathAndDestroy());
    }

    private IEnumerator FlashRed()
    {
        enemyFSM.animationLock = true;
        animator.Play("Get Hit");

        Color original = enemyBody.material.color;

        enemyBody.material.color = Color.red;

        yield return new WaitForSeconds(0.5f);

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
}
