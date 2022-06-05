using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects
{
    public class Item : ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private new string name;
        [TextArea(0, 10)] [SerializeField] private string description;
        [SerializeField] private string id;
        [SerializeField] private Sprite uiImage;
        [SerializeField] private GameObject lootPrefab;

        #endregion

        #region PublicGetters

        public string Name => name;
        public string Description => description;
        public string ID => id;
        public Sprite UIImage => uiImage;

        #endregion

        #region PublicMethods

        public void Drop(int dropAmount)
        {
            if (lootPrefab == null) throw new Exception("Object prefab is null");
            
            Instantiate(lootPrefab);
            
            var lootComponent = lootPrefab.GetComponent<Loot>();
            if (!lootComponent) lootComponent = lootPrefab.AddComponent<Loot>();

            lootComponent.LootDictionary = new Dictionary<Item, int> {{this, dropAmount}};
        }

        #endregion
    }
}
