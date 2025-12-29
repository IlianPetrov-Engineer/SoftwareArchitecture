using SA_Inventory;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    Item item;

    public void RemoveItem()
    {
        Inventory.Instance.RemoveItem(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItems()
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                PlayerStats.Instance.Heal(item.Health);
                break;
        }

        RemoveItem();
    }
}
