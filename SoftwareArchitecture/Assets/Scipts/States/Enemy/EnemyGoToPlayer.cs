using UnityEngine;
using UnityEngine.AI;

public class EnemyGoToPlayer : State
{
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private float distanceThreshold;
    private float targetRange;

    public EnemyGoToPlayer(Transform pTarget, NavMeshAgent pNavMeshAgent, float pDistanceThreshold, float pTargetRange)
    {
        target = pTarget;
        navMeshAgent = pNavMeshAgent;
        distanceThreshold = pDistanceThreshold;
        targetRange = pTargetRange;

        stateName = "GoTo";
    }

    public override void Enter()
    {
        base.Enter();
        //navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
    }

    public override void Step()
    {
        navMeshAgent.SetDestination(target.position);
        base.Step();
    }

    public override void Exit()
    {
        base.Exit();
        //navMeshAgent.enabled = false;
        navMeshAgent.isStopped = true;
    }

    public bool TargetReached()
    {
        return Vector3.Distance(navMeshAgent.transform.position, target.position) <= distanceThreshold;
    }

    public bool TargetOutOfRange()
    {
        return Vector3.Distance(navMeshAgent.transform.position, target.position) > targetRange;
    }
}
