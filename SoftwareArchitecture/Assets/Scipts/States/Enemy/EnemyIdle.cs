using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : State
{
    private Transform self;
    private Transform target;
    private float detectRange;
    private float idleTime;

    private float timer;

    private RandomMovement randomMovement;

    private Animator animator;

    public EnemyIdle(Transform self, Transform target, float detectRange, float idleTime, NavMeshAgent agent, float moveDistance, Animator animator)
    {
        this.self = self;
        this.target = target;
        this.detectRange = detectRange;
        this.idleTime = idleTime;
        this.animator = animator;

        randomMovement = new RandomMovement(self, agent, idleTime, moveDistance);

        stateName = "Idle";
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Step()
    {
        base.Step();

        timer += Time.deltaTime;

        if (IdleTimeOver())
        {
            animator.SetBool("Walk", true);
            randomMovement.Tick();
            
            if (timer > idleTime * 2)
                timer = 0;
        }

        if (!IdleTimeOver())
            animator.SetBool("Walk", false);
    }

    public bool IsTargetInRange()
    {
        return Vector3.Distance(self.transform.position, target.transform.position) <= detectRange;
    }

    public bool IdleTimeOver()
    {
        return timer > idleTime;
    }
}