using UnityEngine;
using System;

namespace SA_Inventory
{
    public class ItemContainer : MonoBehaviour
    {
        public static Action<Item> onGetItem;
        [SerializeField]
        private ItemData itemData;

        public Item GiveItem()
        {
            Item item = itemData.CreateItem();
            onGetItem?.Invoke(item);
            return item;
        }
    }
}
