using UnityEngine;
using UnityEngine.AI;

public class EnemyMaintainDistance : State
{
    private Transform self;
    private Transform target;
    private float chaseRange;
    private float rotationSpeed;
    private NavMeshAgent agent;
    private float minDistance;
    private float maxDistance;
    private EnemyAttackController attackController;

    private Animator animator;

    public EnemyMaintainDistance(Transform self, Transform target, float chaseRange, float rotationSpeed, NavMeshAgent agent, float minDistance, float maxDistance, Animator animator, EnemyAttackController attackController)
    {
        this.self = self;
        this.target = target;
        this.agent = agent;
        this.minDistance = minDistance;
        this.maxDistance = maxDistance;
        this.rotationSpeed = rotationSpeed;
        this.chaseRange = chaseRange;
        this.animator = animator;
        this.attackController = attackController;

        stateName = "MaintainDistance";
    }

    public override void Enter()
    {
        agent.isStopped = false;
    }

    public override void Step()
    {
        base.Step();

        float distance = Vector3.Distance(self.position, target.position);

        Vector3 direction = (target.position - self.position).normalized;

        if (direction != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(self.forward, direction, Vector3.up);
            float step = Mathf.Clamp(angle, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);

            self.Rotate(Vector3.up, step);
        }

        if (distance > maxDistance)
        {
            agent.SetDestination(target.position);
            animator.SetBool("Maintain", true);
            animator.SetBool("Attack", false);
        }

        else if (distance >= minDistance && distance <= maxDistance)
        {
            agent.ResetPath();
            animator.SetBool("Maintain", false);
            animator.SetBool("Attack", true);
            attackController.CanAttack(target);
        }

        if (distance > chaseRange)
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Maintain", false);
        }
    }

    public bool OutOfRange()
    {
        return Vector3.Distance(self.position, target.position) > chaseRange;
    }

    public bool PlayerIsTooClose()
    {
        return Vector3.Distance(self.position, target.position) < minDistance;
    }
}
