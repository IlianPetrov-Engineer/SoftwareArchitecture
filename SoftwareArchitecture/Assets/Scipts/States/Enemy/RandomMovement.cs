using UnityEngine;
using UnityEngine.AI;

public class RandomMovement
{
    private Transform self;
    private NavMeshAgent agent;
    private float moveInterval;
    private float moveDistance;
    private float lastMove;
    private Vector3 moveDirection;

    public RandomMovement(Transform self, NavMeshAgent agent, float moveInterval, float moveDistance)
    {
        this.self = self;
        this.agent = agent;
        this.moveInterval = moveInterval;
        this.moveDistance = moveDistance;
    }

    public void Tick()
    {
        if (Time.time >= lastMove + moveInterval)
            MoveDirection();

        agent.SetDestination(self.position + moveDirection);
    }

    private void MoveDirection()
    {
        lastMove = Time.time;

        Vector3 right = self.right;
        Vector3 forward = self.forward;

        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                moveDirection = right;
                break;

            case 1:
                moveDirection = -right;
                break;

            case 2:
                moveDirection = forward;
                break;

            case 3:
                moveDirection = -forward;
                break;
        }

        moveDirection *= moveDistance;
    }
}
