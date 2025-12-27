using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the hero's attacking behavior, including targeting enemies,
/// launching projectiles, and managing attack animations. This class is an
/// observer to its targetSelector, it subscribe/unsubscribe the targetSelector's
/// events to start/stop attacking.
/// </summary>
public class AttackController : MonoBehaviour
{
    private DamageData damageData; // Holds the current damage data used for attacks
    private float attackInterval; // Time interval between each attack

    [SerializeField]
    private ProjectileController projectilePrefab; // Prefab used to instantiate projectiles

    //[SerializeField]
    //private TargetSelector targetSelector; // Component responsible for selecting enemy targets

    private Coroutine attackCoroutine; // Coroutine used to manage attack timing
    private bool attacking = false; // Indicates whether the hero is currently attacking

    [SerializeField]
    private Vector3 projectileOriginOffest = new Vector3(0f, 1f, 0f); // Offset for projectile spawn position

    [SerializeField]
    private Transform heroModel; // Transform used to rotate the hero toward the target

    [SerializeField]
    private Animator animator; // Animator used to play attack animations

    /// <summary>
    /// Initializes the attack controller with a reference to damage data and the attack interval.
    /// </summary>
    /// <param name="pDamageData">Reference to the damage data structure.</param>
    /// <param name="pAttackInterval">Time between consecutive attacks.</param>
    public void Init(ref DamageData pDamageData, float pAttackInterval)
    {
        damageData = pDamageData;
        attackInterval = pAttackInterval;
    }

    /// <summary>
    /// Starts the attack coroutine when targets are available.
    /// </summary>
    //private void StartAttacking()
    //{
    //    attacking = true;
    //    if (attackCoroutine == null)
    //    {
    //        attackCoroutine = StartCoroutine(Attack());
    //    }
    //}

    ///// <summary>
    ///// Stops the attack coroutine when no targets are found.
    ///// </summary>
    //private void StopAttacking()
    //{
    //    StopCoroutine(attackCoroutine);
    //    attackCoroutine = null;
    //    attacking = false;
    //}

    /// <summary>
    /// Coroutine that continuously attacks while targets are available.
    /// </summary>
    private IEnumerator Attack()
    {
        while (attacking)
        {
            yield return new WaitForSeconds(attackInterval);

            // Get the current target from the selector
            //Transform target = targetSelector.GetTarget();

            // Spawn and set up a projectile toward the target
            ProjectileController projectile = Instantiate<ProjectileController>(projectilePrefab);
            projectile.transform.position = transform.position + projectileOriginOffest;
           // projectile.SetUp(target, damageData);

            // Rotate hero to face the target and play attack animation
            //heroModel.LookAt(target, Vector3.up);
            animator.Play("attack");
        }
    }

    //Observer implementation:

    /// Subscribes to targetSelector's events when this component is enabled.
    //private void OnEnable()
    //{
    //    targetSelector.OnTargetsFound += StartAttacking;
    //    targetSelector.OnTargetsGone += StopAttacking;
    //}

    ///// Unsubscribes from targetSelector's events when this component is disabled.
    //private void OnDisable()
    //{
    //    targetSelector.OnTargetsFound -= StartAttacking;
    //    targetSelector.OnTargetsGone -= StopAttacking;
    //}
}