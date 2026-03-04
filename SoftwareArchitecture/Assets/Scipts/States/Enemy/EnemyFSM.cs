using SA_Enemy;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFSM : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    [Header("Global")]
    [Tooltip("These variables are used for all enemies")]
    [SerializeField] Animator animator;
    public bool animationLock = false;
    [SerializeField] FirstPersonController target;
    private NavMeshAgent agent;
    public EnemyData enemyData;
    [SerializeField] float detectionRange = 1f;
    [SerializeField] float rotateSpeed = 90f;
    [SerializeField] float idleTime = 2f;
    [SerializeField] float moveDistance = 2f;
    [SerializeReference] State currentState;
    [SerializeField] bool playerIsDead = false;

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

    private void OnEnable()
    {
        PlayerStats.Instance.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.onPlayerDeath -= OnPlayerDeath;
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyController.EnemyData.speed;
        target = GameObject.FindAnyObjectByType<FirstPersonController>();

        EnemyAttack mainAttack = enemyController.Attack[0];
        EnemyAttack secondaryAttack = enemyController.Attack.Count > 1 ? enemyController.Attack[1] : enemyController.Attack[0];

        idleState = new EnemyIdle(transform, target.transform, detectionRange, idleTime, agent, moveDistance, animator);
        chaseState = new EnemyChase(transform, target.transform, detectionRange, rotateSpeed, attackRange, agent);
        attackState = new EnemyAttackState(transform, target.transform, attackRange, enemyAttackController, mainAttack);
        escapeState = new EnemyEscape(transform, target.transform, rotateSpeed, agent, safeDistance);
        maintainDistanceState = new EnemyMaintainDistance(transform, target.transform, detectionRange, rotateSpeed, agent, minDistance, maxDistance, animator, enemyAttackController, mainAttack);
        wizardState = new EnemyWizardBahaviour(transform, target.transform, rotateSpeed, detectionRange, attackRange, minDistance, agent, idleTime, moveDistance, enemyAttackController, idleTime, animator, secondaryAttack);

        switch (enemyData.enemyBehaviour)
        {
            case EnemyData.EnemyBehaviour.Melee:
                    MeleeBehaviour();
                    break; 
            
            case EnemyData.EnemyBehaviour.Range:
                RangeBehaviour();
                break;

            case EnemyData.EnemyBehaviour.Aura:
                AuraBehaviour();
                break;

            case EnemyData.EnemyBehaviour.Boss:
                BossBehaviour();
                break;
        }

        currentState = idleState;
        currentState.Enter();
    }

    void Update()
    {
        if (animationLock || playerIsDead)
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

    void BossBehaviour()
    {
        idleState.transitions.Add(new Transition(idleState.IsTargetInRange, wizardState));
        wizardState.transitions.Add(new Transition(wizardState.PlayerIsTooClose, attackState));
        attackState.transitions.Add(new Transition(attackState.TargetOutOfRange, wizardState));
        wizardState.transitions.Add(new Transition(wizardState.PlayerOutOfRange, chaseState));
        chaseState.transitions.Add(new Transition(chaseState.TargetReached, wizardState));
        chaseState.transitions.Add(new Transition(chaseState.TargetOutOfRange, idleState));
    }

    void OnPlayerDeath()
    {
        playerIsDead = true;
        agent.ResetPath();

        currentState.Exit();
        currentState = idleState;
    }
}
