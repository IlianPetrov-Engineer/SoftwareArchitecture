using System.Collections.Generic;
using UnityEngine;

public class DropableItems : MonoBehaviour
{
    [SerializeField] List<GameObject> drops = new List<GameObject>();
    //[SerializeField] GameObject spawner;

    public void Drop()
    {
        //Bounds bounds = spawner.GetComponent<Collider>().bounds;

        //Vector3 randomPosition = new Vector3(
        //    Random.Range(bounds.min.x, bounds.max.x),
        //    Random.Range(bounds.min.y, bounds.max.y),
        //    Random.Range(bounds.min.z, bounds.max.z)
        //    );

        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            int item;
        item = Random.Range(0, drops.Count);

        Instantiate(drops[item], spawnPos, Quaternion.identity);
    }
}
