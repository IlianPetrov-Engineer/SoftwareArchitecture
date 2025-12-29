using System;
using UnityEngine;

namespace SA_Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Unique id for each item")]
        public string id;

        [Header("Core properties")]
        public string itemName;
        public int health;
        public int defense;
        public int value;
        public float buffPercentage;
       // public bool isPotion;

        [Header("Visuals")]
        public Sprite itemIcon;
        public GameObject itemModel;

        public Item CreateItem()
        {
            return new Item(this);
        }
    }

    [Serializable]
    public class Item
    {
        [Header("Unique id for each item")]
        [SerializeField]
        private string id;
        public string Id => id;

        [Header("Core properties")]
        [SerializeField]
        private string itemName;
        public string ItemName => itemName;
        [SerializeField]
        private int health;
        public int Health => health;
        [SerializeField]
        private int defense;
        public int Defense => defense;
        [SerializeField]
        private int value;
        public int Value => value;
        [SerializeField]
        private float buffPercentage;
        public float BuffPercentage => buffPercentage;

        //[SerializeField]
        //private bool isPotion;
        //public bool IsPotion => isPotion;

        [Header("Visuals")]
        public Sprite itemIcon;
        public GameObject itemModel;

        public Item(ItemData itemData)
        {
            id = itemData.id;
            itemName = itemData.itemName;
            health = itemData.health;
            defense = itemData.defense;
            value = itemData.value;
            buffPercentage = itemData.buffPercentage;

            itemIcon = itemData.itemIcon;
            itemModel = itemData.itemModel;
            //isPotion = itemData.isPotion;
        }

        public ItemType itemType;
        public enum ItemType
        {
            HealthPotion
        }
    }
}
