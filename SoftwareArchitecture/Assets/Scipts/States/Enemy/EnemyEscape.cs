using UnityEngine;
using UnityEngine.AI;

public class EnemyEscape : State
{
    private Transform self;
    private Transform target;
    private float rotationSpeed;
    private NavMeshAgent agent;
    private float safeDistance;

    public EnemyEscape (Transform self, Transform target, float rotationSpeed, NavMeshAgent agent, float safeDistance)
    {
        this.self = self;
        this.target = target;
        this.rotationSpeed = rotationSpeed;
        this.agent = agent;
        this.safeDistance = safeDistance;

        stateName = "Escape";
    }

    public override void Enter()
    {
        base.Enter();
        agent.isStopped = false;
    }

    public override void Step()
    {
        base.Step();   

        Vector3 direction = (self.position - target.position).normalized;
        Vector3 escapePos = self.position + direction * safeDistance;

        if (direction != Vector3.zero)
        {
            Vector3 dir = Vector3.ProjectOnPlane(direction, Vector3.up);
            float angle = Vector3.SignedAngle(self.forward, dir, Vector3.up);
            float step = Mathf.Clamp(angle, -rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);

            self.Rotate(Vector3.up, step);
        }

        agent.SetDestination(escapePos);

        if (SafeDistanceReached())
            agent.ResetPath();
    }

    public bool SafeDistanceReached()
    {
        return Vector3.Distance(self.position, target.position) >= safeDistance;
    }
}
