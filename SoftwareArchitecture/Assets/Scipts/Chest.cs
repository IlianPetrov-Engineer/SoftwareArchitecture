using UnityEngine;
using SA_Inventory;

public class Chest : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerExtras.onInteract += Open;
    }

    public void Open()
    {
        ItemContainer itemContainer = GetComponent<ItemContainer>();
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
