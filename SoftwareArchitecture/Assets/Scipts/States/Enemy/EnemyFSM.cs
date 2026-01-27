using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private FirstPersonController target;
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private float chaseRange = 3f;
    [SerializeField]
    private float chaseThreshold = 1f;
    [SerializeField]
    private float attackRange = 1.5f;
    [SerializeField]
    private float rotateSpeed = 90f;
    [SerializeField]
    private float idleTime = 2f;
    /*[SerializeField]
    private Animator animator;*/

    private EnemyGoToPlayer chaseState;
    private EnemyFacePlayer faceState;
    private EnemyIdle enemyIdle;

    [SerializeReference]
    private State currentState;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindAnyObjectByType<FirstPersonController>();

        chaseState = new EnemyGoToPlayer(target.GetComponent<Transform>(), navMeshAgent, chaseThreshold, chaseRange);
        faceState = new EnemyFacePlayer(transform, target.GetComponent<Transform>(), rotateSpeed, attackRange);
        enemyIdle = new EnemyIdle(chaseRange, transform, target.GetComponent<Transform>(), idleTime);

        enemyIdle.transitions.Add(new Transition(enemyIdle.IsTargetInRange, chaseState));

        chaseState.transitions.Add(new Transition(chaseState.TargetReached, faceState));
        chaseState.transitions.Add(new Transition(chaseState.TargetOutOfRange, enemyIdle));

        faceState.transitions.Add(new Transition(faceState.TargetOutOfRange, chaseState));
        //faceState.transitions.Add(new Transition(faceState.AlignedWithTarget, attackState));

        //attackState.transitions.Add(new Transition(attackState.AttackIsOver, faceState));


        /*idleState.onEnter += () => { animator.SetBool("Idle", true); };
        idleState.onExit += () => { animator.SetBool("Idle", false); };
        moveToState.onEnter += () => { animator.SetBool("Chase", true); };
        moveToState.onExit += () => { animator.SetBool("Chase", false); };
        alignToState.onEnter += () => { animator.SetBool("Aim", true); };
        alignToState.onExit += () => { animator.SetBool("Aim", false); };*/

        currentState = enemyIdle;
        currentState.Enter();
    }

    void Update()
    {
        currentState.Step();
        State nextState = currentState.NextState();

        if (nextState != null)
        {
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }
    }
}
