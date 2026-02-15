using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : State
{
    private Transform self;
    private Transform target;
    private float chaseRange;
    private float rotationSpeed;
    private float attackRange;
    private NavMeshAgent agent;

    public EnemyChase(Transform self, Transform target, float chaseRange, float rotationSpeed, float attackRange, NavMeshAgent agent)
    {
        this.self = self;
        this.target = target;
        this.chaseRange = chaseRange;
        this.rotationSpeed = rotationSpeed;
        this.attackRange = attackRange;
        this.agent = agent;

        stateName = "Chase";
    }

    public override void Enter()
    {
        base.Enter();
        agent.isStopped = false;
    }

    public override void Step()
    {
        base.Step();

        agent.SetDestination(target.position);

        Vector3 direction = (target.position - self.position).normalized;

        if (direction != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(self.forward, direction, Vector3.up);
            float step = Mathf.Clamp(angle, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);

            self.Rotate(Vector3.up, step);
        }
    }

    //public override void Exit()
    //{
    //    //base.Exit();
    //    //navMeshAgent.enabled = false;
    //    agent.isStopped = true;
    //}

    public bool TargetReached()
    {
        return Vector3.Distance(self.transform.position, target.position) <= attackRange;
    }

    public bool TargetOutOfRange()
    {
        return Vector3.Distance(self.transform.position, target.position) > chaseRange;
    }
}
