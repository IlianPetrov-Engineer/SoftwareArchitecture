using UnityEngine;

public class EnemyFacePlayer : State
{
    private Transform target;
    private Transform self;
    private Vector3 direction;
    private float rotationSpeed;
    private float rotationSign;
    private float targetRange;

    public EnemyFacePlayer(Transform pSelf, Transform pTarget, float pRotationSpeed, float pTargetRange)
    {
        self = pSelf;
        SetTarget(pTarget);
        rotationSpeed = pRotationSpeed;
        targetRange = pTargetRange;

        stateName = "FacePlayer";
    }

    public void SetTarget(Transform pTarget)
    {
        target = pTarget;
    }


    public override void Step()
    {
        base.Step();

        direction = (target.position - self.position).normalized;

        rotationSign = Mathf.Sign(Vector3.Dot(self.right, direction));

        self.Rotate(self.up, rotationSign * rotationSpeed * Time.deltaTime);
    }

    public bool AlignedWithTarget()
    {
        return Vector3.Dot(self.forward, direction) >= 0.95f;
    }

    public bool TargetOutOfRange()
    {
        return Vector3.Distance(self.position, target.position) > targetRange;
    }
}
