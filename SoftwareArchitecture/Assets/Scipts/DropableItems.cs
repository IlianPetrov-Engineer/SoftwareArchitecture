using System.Collections.Generic;
using UnityEngine;

public class DropableItems : MonoBehaviour
{
    [SerializeField] List<GameObject> drops = new List<GameObject>();

    public void Drop()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        int item;
        item = Random.Range(0, drops.Count);

        Instantiate(drops[item], spawnPos, Quaternion.identity);
    }
}
