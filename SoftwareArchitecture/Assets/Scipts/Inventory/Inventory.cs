using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

namespace SA_Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [SerializeField] Transform itemContent;
        [SerializeField] GameObject inventoryItem;

        public UseItem[] useItems;

        //// List of item data assets used to generate actual items at runtime.
        //[SerializeField]
        //private List<ItemData> itemDatas;

        // List of instantiated items currently in the inventory.
        [SerializeReference]
        private List<Item> items;

        // Public read-only property to access a copy of the items list.
        public Item[] Items => items.ToArray();

        //// MonoBehaviour-based sorting strategies.
        //[SerializeField]
        //private ItemSortingStrategy[] itemSortingStrategies;

        //// Index of the currently active sorting strategy.
        //[SerializeField]
        //private int strategyIndex = 0;

        private void Awake()
        {
            GenerateInventory();             // Create items based on itemDatas.
            //LoadItemSortingStrategies();     // Find sorting strategies attached as components.
            Instance = this;
        }

        private void OnEnable()
        {
            ItemCreation.onGetItem += AddItem;
        }

        // Instantiates items based on the item data list.
        private void GenerateInventory()
        {
            items = new List<Item>();
            //foreach (ItemData itemData in itemDatas)
            //{
            //    items.Add(itemData.CreateItem()); // Create an item from its data.
            //}
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void ListItems()
        {
            foreach (Transform item in itemContent)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in Items)
            {
                GameObject obj = Instantiate(inventoryItem, itemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                itemName.text = item.ItemName;
                itemIcon.sprite = item.itemIcon;
            }

            SetInventoryItems();
        }

        public void SetInventoryItems()
        {
            useItems = itemContent.GetComponentsInChildren<UseItem>();

            for (int i = 0; i < items.Count; i++)
            {
                useItems[i].AddItem(items[i]);
            }
        }

        //#region "Strategy Pattern Implementation"
        //// Loads sorting strategy components from child objects.
        //private void LoadItemSortingStrategies()
        //{
        //    itemSortingStrategies = GetComponentsInChildren<ItemSortingStrategy>();
        //}

        //// Returns the items sorted according to the current strategy.
        //public Item[] GetSortedItems()
        //{
        //    // If no sorting strategies exist, return the unsorted list.
        //    if (itemSortingStrategies.Length == 0)
        //    {
        //        return items.ToArray();
        //    }
        //    else
        //    {
        //        return itemSortingStrategies[strategyIndex].GetSortedItems(items);
        //    }
        //}

        //// Sets the current sorting strategy by index.
        //public void SetSortingStrategy(int pIndex)
        //{
        //    strategyIndex = pIndex;
        //}

        //// Cycles to the next sorting strategy (loops back to 0 if at the end).
        //public void NextSortingStrategy()
        //{
        //    if (strategyIndex == itemSortingStrategies.Length - 1)
        //    {
        //        strategyIndex = 0;
        //    }
        //    else
        //    {
        //        strategyIndex++;
        //    }
        //}

        //// Cycles to the previous sorting strategy (loops to last if at the start).
        //public void PreviousSortingStrategy()
        //{
        //    if (strategyIndex == 0)
        //    {
        //        strategyIndex = itemSortingStrategies.Length - 1;
        //    }
        //    else
        //    {
        //        strategyIndex--;
        //    }
        //}

        //// Returns the name of the currently selected sorting strategy.
        //public string GetCurrentStrategyName()
        //{
        //    return itemSortingStrategies[strategyIndex].StrategyName;
        //}
        //#endregion
    }
}