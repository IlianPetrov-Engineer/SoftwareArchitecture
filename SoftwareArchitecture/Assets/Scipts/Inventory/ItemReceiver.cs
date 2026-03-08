using UnityEngine;
using SA_Inventory;

public class ItemReceiver : MonoBehaviour
{
    public void Collect()
    {
        ItemCreation itemContainer = GetComponent<ItemCreation>();
        if (itemContainer != null)
        {
            Item item = itemContainer.GiveItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();

            GameObject.Destroy(gameObject);
        }
    }
}