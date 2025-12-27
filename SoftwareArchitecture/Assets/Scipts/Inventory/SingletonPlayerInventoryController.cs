using UnityEngine;
using SA_Inventory;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(Inventory))]

public class SingletonPlayerInventoryController : MonoBehaviour
{
    public Inventory inventory { get; private set; }

    public static SingletonPlayerInventoryController Instance { get; private set; }

    [SerializeField] Transform itemContent;
    [SerializeField] GameObject inventoryItem;

    [SerializeField] GameObject inventoryPresenter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if(inventory == null)
        {
            inventory = GetComponent<Inventory>();
        }

        if(inventory != null)
        {
            ItemCreation.onGetItem += inventory.AddItem;
        }
    }

    private void OnDisable()
    {
        if (inventory != null)
        {
            ItemCreation.onGetItem -= inventory.AddItem;
        }
    }

    private void Update()
    {
        if (inventoryPresenter.activeSelf)
        {
            ListItems();
        }
    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in inventory.Items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            //inventoryItem.GetComponent<Button>().onClick.AddListener(() => inventory.UseItem());
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.itemIcon;
        }
    }
}
