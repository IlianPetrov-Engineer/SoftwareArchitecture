using SA_Enemy;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private int damage;

    public void Initialise(int damageAmount, float lifetime)
    {
        damage = damageAmount;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerStats player = collision.collider.GetComponent<PlayerStats>();

        if (player != null)
            player.TakeDamage(damage);

        Debug.Log(player.ToString());

        Destroy(gameObject);
    }
}
