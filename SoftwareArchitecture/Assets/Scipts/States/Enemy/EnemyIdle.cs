using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : State
{
    private Transform self;
    private Transform target;
    private float detectRange;
    private float idleTime;
    
    private float startTime;

    private RandomMovement randomMovement;

    public EnemyIdle(Transform self, Transform target, float detectRange, float idleTime, NavMeshAgent agent, float moveInterval, float moveDistance)
    {
        this.self = self;
        this.target = target;
        this.detectRange = detectRange;
        this.idleTime = idleTime;

        randomMovement = new RandomMovement(self, agent, moveInterval, moveDistance);

        stateName = "Idle";
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }

    public override void Step()
    {
        base.Step();

        if (IdleTimeOver())
            randomMovement.Tick();
    }

    public bool IsTargetInRange()
    {
        return Vector3.Distance(self.transform.position, target.transform.position) <= detectRange;
    }

    public bool IdleTimeOver()
    {
        return Time.time > startTime + idleTime;
    }
}
    