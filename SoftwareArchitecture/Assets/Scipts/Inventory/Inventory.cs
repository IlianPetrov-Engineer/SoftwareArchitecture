using System.Collections.Generic;
//using TMPro;
//using UnityEngine.UI;
using UnityEngine;
//using System.Linq;
using System;

namespace SA_Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        private List<Item> items;

        public Item[] Items => items.ToArray();

        public event Action onInventoryChange;

        //// MonoBehaviour-based sorting strategies.
        //[SerializeField]
        //private ItemSortingStrategy[] itemSortingStrategies;

        //// Index of the currently active sorting strategy.
        //[SerializeField]
        //private int strategyIndex = 0;

        private void Awake()
        {
            items = new List<Item>();           
            //LoadItemSortingStrategies();     // Find sorting strategies attached as components.
            Instance = this;
        }

        private void OnEnable()
        {
            ItemCreation.onGetItem += AddItem;
        }

        private void OnDisable()
        {
            ItemCreation.onGetItem -= AddItem;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            onInventoryChange?.Invoke();
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
            onInventoryChange?.Invoke();
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