using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private DamageContext context;

    public void Initialise(DamageContext context, float lifetime)
    {
        this.context = context;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController enemy = collision.collider.GetComponent<EnemyController>();

        if (enemy != null)
            enemy.GetHit(context);

        Destroy(gameObject);
    }
}
