using UnityEngine;

public class EnemyIdle : State
{
    private float detectRange;
    private Transform self;
    private Transform target;
    private float idleTime;
    private float startTime;

    public EnemyIdle(float pDetectRange, Transform pSelf, Transform pTarget, float pIdleTime)
    {
        detectRange = pDetectRange;
        self = pSelf;
        target = pTarget;
        idleTime = pIdleTime;

        stateName = "Idle";
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }

    public bool IsTargetInRange()
    {
        return (Vector3.Distance(self.transform.position, target.transform.position) <= detectRange);
    }

    public bool IdleTimeOver()
    {
        return Time.time > startTime + idleTime;
    }
}
