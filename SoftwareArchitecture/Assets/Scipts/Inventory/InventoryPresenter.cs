using SA_Inventory;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryView inventoryView;

    private void OnEnable()
    {
        inventory.onInventoryChange += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        inventory.onInventoryChange -= Refresh;
    }

    private void Refresh()
    {
        inventoryView.ListItems(inventory.Items);
    }
}
