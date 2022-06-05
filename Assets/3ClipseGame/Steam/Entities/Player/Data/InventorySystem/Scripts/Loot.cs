using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private List<LootElement> loot;

        public Dictionary<Item, int> LootDictionary;

        private void Start()
        {
            foreach (var lootElement in loot)
            {
                var lootAmount = lootElement.GetFinalAmount();
                if(lootAmount == 0) continue;
                
                LootDictionary.Add(lootElement.item, lootAmount);
            }
        }
    }

    [System.Serializable]
    public struct LootElement
    {
        public Item item;
        [SerializeField] private int minimumDropAmount;
        [SerializeField] private int maximumDropAmount;
        [Range(0,100)] [SerializeField] private float eachItemDropChance;

        public int GetFinalAmount()
        {
            var result = minimumDropAmount;
            var iterator = minimumDropAmount;
            
            while (iterator != maximumDropAmount)
            {
                var random = Random.Range(0, 100);
                if (random < eachItemDropChance) result++;
                iterator++;
            }

            return result;
        }
    }
}
