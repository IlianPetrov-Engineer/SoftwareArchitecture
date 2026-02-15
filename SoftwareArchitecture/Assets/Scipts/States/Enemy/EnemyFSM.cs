using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Melee,
    Range,
    Aura
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFSM : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    [Header("Global")]
    [Tooltip("These variables are used for all enemies")]
    /*[SerializeField]
    private Animator animator;*/
    [SerializeField] FirstPersonController target;
    private NavMeshAgent agent;
    [SerializeField] EnemyType enemyType;
    [SerializeField] float detectionRange = 1f;
    [SerializeField] float rotateSpeed = 90f;
    [SerializeField] float idleTime = 2f;
    [SerializeField] float moveInterval = 2f;
    [SerializeField] float moveDistance = 2f;
    [SerializeReference] private State currentState;

    [Header("Melee && Range")]
    [Tooltip("These variables are used for the melee and range enemy.")]
    [SerializeField] EnemyAttackController enemyAttackController;
    [SerializeField] float attackRange = 1.5f;

    [Header("Melee && Aura")]
    [Tooltip("These variable is used for the melee and aura enemy.")]
    [SerializeField] float chaseRange = 3f;

    [Header("Range && Aura")]
    [Tooltip("These variables are used for the range and aura enemy.")]
    [SerializeField] float safeDistance;
    [SerializeField] float minDistance;

    [Header("Aura")]
    [Tooltip("These variable is used for the aura enemy.")]
    [SerializeField] float maxDistance;

    private EnemyIdle idleState;
    private EnemyChase chaseState;
    private EnemyAttackState attackState;
    private EnemyEscape escapeState;
    private EnemyMaintainDistance maintainDistanceState;
    private EnemyWizardBahaviour wizardState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyController.EnemyData.speed;
        target = GameObject.FindAnyObjectByType<FirstPersonController>();

        idleState = new EnemyIdle(transform, target.transform, detectionRange, idleTime, agent, moveInterval, moveDistance);
        chaseState = new EnemyChase(transform, target.transform, chaseRange, rotateSpeed, attackRange, agent);
        attackState = new EnemyAttackState(transform, target.transform, attackRange, enemyAttackController, moveInterval);
        escapeState = new EnemyEscape(transform, target.transform, rotateSpeed, agent, safeDistance);
        maintainDistanceState = new EnemyMaintainDistance(transform, target.transform, chaseRange, rotateSpeed, agent, minDistance, maxDistance);
        wizardState = new EnemyWizardBahaviour(transform, target.transform, rotateSpeed, detectionRange, attackRange, minDistance, agent, moveInterval, moveDistance, enemyAttackController);

        /*idleState.onEnter += () => { animator.SetBool("Idle", true); };
        idleState.onExit += () => { animator.SetBool("Idle", false); };
        moveToState.onEnter += () => { animator.SetBool("Chase", true); };
        moveToState.onExit += () => { animator.SetBool("Chase", false); };
        alignToState.onEnter += () => { animator.SetBool("Aim", true); };
        alignToState.onExit += () => { animator.SetBool("Aim", false); };*/

        switch (enemyType)
        {
            case EnemyType.Melee:
                MeleeBehaviour();
                break; 
            
            case EnemyType.Range:
                RangeBehaviour();
                break;

            case EnemyType.Aura:
                AuraBehaviour();
                break;
        }

        currentState = idleState;
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

    void MeleeBehaviour()
    {
        idleState.transitions.Add(new Transition(idleState.IsTargetInRange, chaseState));
        chaseState.transitions.Add(new Transition(chaseState.TargetReached, attackState));
        attackState.transitions.Add(new Transition(attackState.TargetOutOfRange, chaseState));
        chaseState.transitions.Add(new Transition(chaseState.TargetOutOfRange, idleState));
    }

    void RangeBehaviour()
    {
        idleState.transitions.Add(new Transition(idleState.IsTargetInRange, wizardState));
        wizardState.transitions.Add(new Transition(wizardState.PlayerIsTooClose, escapeState));
        escapeState.transitions.Add(new Transition(escapeState.SafeDistanceReached, wizardState));
    }

    void AuraBehaviour()
    {
        idleState.transitions.Add(new Transition(idleState.IsTargetInRange, maintainDistanceState));
        maintainDistanceState.transitions.Add(new Transition(maintainDistanceState.PlayerIsTooClose, escapeState));
        maintainDistanceState.transitions.Add(new Transition(maintainDistanceState.OutOfRange, idleState));
        escapeState.transitions.Add(new Transition(escapeState.SafeDistanceReached, maintainDistanceState));
    }
}
