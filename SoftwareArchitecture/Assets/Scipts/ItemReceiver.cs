using UnityEngine;
using SA_Inventory;

public class ItemReceiver : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerExtras.onInteract += Open;
    }

    public void Open()
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
            Open();

            GameObject.Destroy(gameObject);
        }
    }
}
