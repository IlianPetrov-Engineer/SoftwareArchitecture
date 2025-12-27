using System.Collections.Generic;
using UnityEngine;

public class DropableItems : MonoBehaviour
{
    [SerializeField] List<GameObject> drops = new List<GameObject>();
    //[SerializeField] XP xp;

    //private void Awake()
    //{
    //    if (xp == null)
    //    {
    //        Debug.LogError("Enemy is missing `Xp` prefab");
    //    }
    //}

    public void Drop()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        int item;
        item = Random.Range(0, drops.Count);

        //Instantiate(xp, spawnPos, Quaternion.identity);
        Instantiate(drops[item], spawnPos, Quaternion.identity);
    }
}
