using SA_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] Transform itemContent;
    [SerializeField] GameObject inventoryItem;

    public void ListItems(Item[] items)
    {
        foreach(Transform item in itemContent)
            Destroy(item.gameObject);

        foreach(var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);

            obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.ItemName;
            obj.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;

            obj.GetComponent<UseItem>().AddItem(item);
        }
    }
}
