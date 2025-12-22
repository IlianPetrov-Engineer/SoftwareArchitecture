using System.Collections.Generic;
using UnityEngine;
using System;

namespace SA_Inventory
{
    public abstract class ItemSortingStrategy : MonoBehaviour
    {
        [SerializeField]
        protected string strategyName;
        public string StrategyName => strategyName;
        public abstract Item[] GetSortedItems(List<Item> items);
    }
}
