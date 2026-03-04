using UnityEngine;
using UnityEngine.AI;

public class EnemyWizardBahaviour : State
{
    private Transform self;
    private Transform target;
    private float rotationSpeed;
    private float detectionRange;
    private float attackRange;
    private float dangerRange;
    private NavMeshAgent agent;
    private EnemyAttackController attackController;
    private float stopAttack;

    private RandomMovement randomMovement;

    private Animator animator;

    private EnemyAttack enemyAttack;

    private float timer;
    private bool isAttacking;

    public EnemyWizardBahaviour(Transform self, Transform target, float rotationSpeed, float detectionRange, float attackRange, float dangerRange, NavMeshAgent agent, float moveInterval, float moveDistance, EnemyAttackController attackController, float stopAttack, Animator animator, EnemyAttack enemyAttack)
    {
        this.self = self;
        this.target = target;
        this.agent = agent;
        this.attackRange = attackRange;
        this.dangerRange = dangerRange;
        this.rotationSpeed = rotationSpeed;
        this.detectionRange = detectionRange;
        this.attackController = attackController;
        this.stopAttack = stopAttack;
        this.animator = animator;
        this.enemyAttack = enemyAttack;
        
        randomMovement = new RandomMovement(self, agent, moveInterval, moveDistance);

        stateName = "RapidMovement";
    }

    public override void Enter()
    {
        base.Enter();
        agent.isStopped = false;
        isAttacking = false;
    }

    public override void Step()
    {
        base.Step();

        Vector3 direction = (target.position - self.position).normalized;

        if (direction != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(self.forward, direction, Vector3.up);
            float step = Mathf.Clamp(angle, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);

            self.Rotate(Vector3.up, step);
        }

        float distance = Vector3.Distance(self.position, target.position);

        if (distance <= attackRange && distance >= dangerRange)
        {
            timer += Time.deltaTime;

            if (!isAttacking)
            {
                randomMovement.Tick();

                if (timer >= stopAttack / 2)
                {
                    isAttacking = true;
                    timer = 0;
                    animator.SetBool("Attack", true);
                    animator.SetBool("Walk", false);
                }
            }

            else
            {
                attackController.CanAttack(target, enemyAttack);

                if (timer >= stopAttack)
                {
                    isAttacking = false;
                    timer = 0;
                    animator.SetBool("Attack", false);
                    animator.SetBool("Walk", true);
                }
            }
        }

        else
        {
            isAttacking = false;
            timer = 0;
            animator.SetBool("Attack", false);
        }
    }

    public bool PlayerIsTooClose()
    {
        return Vector3.Distance(self.position, target.position) < dangerRange;
    }

    public bool PlayerOutOfRange()
    {
        return Vector3.Distance(self.position, target.position) > detectionRange;
    }
}
