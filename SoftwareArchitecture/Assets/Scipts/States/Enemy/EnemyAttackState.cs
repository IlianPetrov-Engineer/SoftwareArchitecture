using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : State
{
    private Transform self;
    private Transform target;
    private float attackRange;
    private EnemyAttackController attackController;
    private EnemyAttack enemyAttack;

    public EnemyAttackState(Transform self, Transform target, float attackrange, EnemyAttackController attackController, EnemyAttack enemyAttack)
    {
        this.self = self;
        this.target = target;
        this.attackRange = attackrange;
        this.attackController = attackController;
        this.enemyAttack = enemyAttack;

        stateName = "Attack";
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Step()
    {
        float distance = Vector3.Distance(self.position, target.position);

        if (distance <= attackRange)
        {
            attackController.CanAttack(target, enemyAttack);
        }
    }

    public bool TargetOutOfRange()
    {
        return Vector3.Distance(self.position, target.position) > attackRange;
    }
}
