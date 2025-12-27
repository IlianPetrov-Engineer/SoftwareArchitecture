using SA_Inventory;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    private Item item;
    private ItemData data;
    private PlayerStats playerStats;
    //private Inventory inventory;

    void RemoveItem()
    {
        Inventory.Instance.RemoveItem(item);
    }

    public void UseItems()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.HealthPotion:
                playerStats.Heal(data.health);
                break;

        }

       RemoveItem();
    }
}
