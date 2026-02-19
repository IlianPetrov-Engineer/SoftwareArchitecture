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
    [SerializeField] private Animator animator;
    public bool animationLock = false;
    [SerializeField] FirstPersonController target;
    private NavMeshAgent agent;
    public EnemyType enemyType;
    [SerializeField] float detectionRange = 1f;
    [SerializeField] float rotateSpeed = 90f;
    [SerializeField] float idleTime = 2f;
    [SerializeField] float moveDistance = 2f;
    [SerializeReference] private State currentState;

    [Header("Melee && Range")]
    [Tooltip("These variables are used for the melee and range enemy.")]
    [SerializeField] EnemyAttackController enemyAttackController;
    [SerializeField] float attackRange = 1.5f;

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

        idleState = new EnemyIdle(transform, target.transform, detectionRange, idleTime, agent, moveDistance, animator);
        chaseState = new EnemyChase(transform, target.transform, detectionRange, rotateSpeed, attackRange, agent);
        attackState = new EnemyAttackState(transform, target.transform, attackRange, enemyAttackController);
        escapeState = new EnemyEscape(transform, target.transform, rotateSpeed, agent, safeDistance);
        maintainDistanceState = new EnemyMaintainDistance(transform, target.transform, detectionRange, rotateSpeed, agent, minDistance, maxDistance, animator, enemyAttackController);
        wizardState = new EnemyWizardBahaviour(transform, target.transform, rotateSpeed, detectionRange, attackRange, minDistance, agent, idleTime, moveDistance, enemyAttackController, idleTime, animator);

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
        if (animationLock)
            return;

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

        idleState.onExit += () => { animator.SetBool("Walk", false); };
        chaseState.onEnter += () => { animator.SetBool("Chase", true); };
        chaseState.onExit += () => { animator.SetBool("Chase", false); };
        attackState.onEnter += () => { animator.Play("Attack01 0"); };
        attackState.onEnter += () => { animator.SetBool("Attack", true); };
        attackState.onExit += () => {animator.SetBool("Attack", false); };
        
    }

    void RangeBehaviour()
    {
        idleState.transitions.Add(new Transition(idleState.IsTargetInRange, wizardState));
        wizardState.transitions.Add(new Transition(wizardState.PlayerIsTooClose, escapeState));
        wizardState.transitions.Add(new Transition(wizardState.PlayerOutOfRange, idleState));
        escapeState.transitions.Add(new Transition(escapeState.SafeDistanceReached, wizardState));

        idleState.onExit += () => { animator.SetBool("Walk", false); };
        wizardState.onEnter += () => { animator.SetBool("Walk", true); };
        wizardState.onExit += () => { animator.SetBool("Attack", false); };
        escapeState.onEnter += () => { animator.SetBool("Walk", true); };
        escapeState.onExit += () => { animator.SetBool("Walk", false); };
    }

    void AuraBehaviour()
    {
        idleState.transitions.Add(new Transition(idleState.IsTargetInRange, maintainDistanceState));
        maintainDistanceState.transitions.Add(new Transition(maintainDistanceState.PlayerIsTooClose, escapeState));
        maintainDistanceState.transitions.Add(new Transition(maintainDistanceState.OutOfRange, idleState));
        escapeState.transitions.Add(new Transition(escapeState.SafeDistanceReached, maintainDistanceState));

        idleState.onExit += () => { animator.SetBool("Walk", false); };
        escapeState.onEnter += () => { animator.SetBool("Maintain", true); };
        escapeState.onEnter += () => { animator.SetBool("Attack", false); };
        escapeState.onExit += () => { animator.SetBool("Maintain", false); };
    }
}
