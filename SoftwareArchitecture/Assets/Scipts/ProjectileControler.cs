using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private DamageData damageData;

    public void Initialise(DamageData data, float lifetime)
    {
        damageData = data;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController enemy = collision.collider.GetComponent<EnemyController>();

        if (enemy != null)
            enemy.GetHit(damageData);

        Destroy(gameObject);
    }
}
