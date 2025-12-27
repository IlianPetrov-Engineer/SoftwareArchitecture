using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private DamageData damageData;

    public List<GameObject> activeEnemies = new List<GameObject>();

    private float speed = 1f;

    private float duration = 5f;

    public void Attack()
    {
        Vector3 movement = (transform.forward - transform.position).normalized * speed * Time.deltaTime;
        transform.position += movement;

        Destroy(gameObject, duration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            activeEnemies.Add(collision.gameObject);

            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            DropableItems dropableItems = collision.gameObject.GetComponent<DropableItems>();
            if (enemyController != null)
            {
                enemyController.XP();
                //enemyController.GetHit(damageData);
                dropableItems.Drop();
                activeEnemies.Remove(collision.gameObject);
                GameObject.Destroy(collision.gameObject);
            }
        }
    }


}
