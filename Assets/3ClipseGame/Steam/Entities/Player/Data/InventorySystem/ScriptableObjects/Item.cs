using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects
{
    public class Item : ScriptableObject
    {
        [SerializeField] private new string name;
        [TextArea(0, 10)] [SerializeField] private string description;
        [SerializeField] private string id;
        [SerializeField] private Sprite uiImage;
        [SerializeField] private GameObject lootPrefab;

        public string Name => name;
        public string Description => description;
        public string ID => id;
        public Sprite UIImage => uiImage;

        public void Drop(int dropAmount)
        {
            if (lootPrefab == null) throw new Exception("Object prefab is null");
            
            Instantiate(lootPrefab);
            
            var lootComponent = lootPrefab.GetComponent<Loot>();
            if (!lootComponent) lootComponent = lootPrefab.AddComponent<Loot>();

            lootComponent.LootDictionary = new Dictionary<Item, int> {{this, dropAmount}};
        }
    }
}
