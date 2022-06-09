using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    [System.Serializable]
    public struct LootElement
    {
        #region SerializeFields

        public Item item;
        [SerializeField] private int minimumDropAmount;
        [SerializeField] private int maximumDropAmount;
        [Range(0,100)] [SerializeField] private float eachItemDropChance;

        #endregion

        #region PublicMethods

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

        #endregion
    }
}