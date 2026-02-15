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

    private RandomMovement randomMovement;

    public EnemyWizardBahaviour(Transform self, Transform target, float rotationSpeed, float detectionRange, float attackRange, float dangerRange, NavMeshAgent agent, float moveInterval, float moveDistance, EnemyAttackController attackController)
    {
        this.self = self;
        this.target = target;
        this.agent = agent;
        this.attackRange = attackRange;
        this.dangerRange = dangerRange;
        this.rotationSpeed = rotationSpeed;
        this.detectionRange = detectionRange;
        this.attackController = attackController;
        
        randomMovement = new RandomMovement(self, agent, moveInterval, moveDistance);

        stateName = "RapidMovement";
    }

    public override void Enter()
    {
        base.Enter();
        agent.isStopped = false;
    }

    public override void Step()
    {
        base.Step();

        FacePlayer();

        randomMovement.Tick();

        float distance = Vector3.Distance(self.position, target.position);

        if (distance <= attackRange && distance >= dangerRange)
            attackController.CanAttack(target);

        if (distance > detectionRange)
            agent.SetDestination(target.position);
    }

    private void FacePlayer()
    {
        Vector3 direction = (target.position - self.position).normalized;

        if (direction != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(self.forward, direction, Vector3.up);
            float step = Mathf.Clamp(angle, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);

            self.Rotate(Vector3.up, step);
        }
    }

    public bool PlayerIsTooClose()
    {
        return Vector3.Distance(self.position, target.position) < dangerRange;
    }


    public bool CanAttackPlayer()
    {
        float distance = Vector3.Distance(self.position, target.position);
        return distance <= attackRange && distance >= dangerRange;
    }

    public bool PlayerOutOfRange()
    {
        
        return Vector3.Distance(self.position, target.position) > detectionRange;
    }
}
