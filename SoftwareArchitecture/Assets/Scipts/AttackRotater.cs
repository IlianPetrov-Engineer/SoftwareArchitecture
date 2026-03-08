using UnityEngine;

public class AttackRotater : MonoBehaviour
{
    [SerializeField] Vector3 axis = new Vector3 (0, 0, 1);
    [SerializeField] float speed;
    [SerializeField] PlayerAttacks playerAttacks;

    float targetAngle;

    void Update()
    {
        Attacks currentAttack = playerAttacks.GetSelectedAbility();

        targetAngle = GetTargetAngle(currentAttack);

        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, axis);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, speed * Time.deltaTime);

    }

    float GetTargetAngle(Attacks currentAttack)
    {
        if (currentAttack is FireballAttack fireball)
            return 0f;

        if (currentAttack is FreezeAttack freeze)
            return 90;

        if (currentAttack is GravityPushAttack gravity)
            return 180;

        return 0f;
    }
}
