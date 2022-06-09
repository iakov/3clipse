using System;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem
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
        public ItemType TypeOfItem { get; set; }

        #endregion

        #region PublicMethods

        public void DropOnGround(int dropAmount)
        {
            if (lootPrefab == null) throw new Exception("Object prefab is null");
            if (!lootPrefab.TryGetComponent<Rigidbody>(out var rigidbody)) throw new Exception("Prefab must have Rigidbody on it");
            if (!Instantiate(lootPrefab).TryGetComponent<PickableLoot>(out var lootComponent))
                    throw new Exception("Prefab must have Loot component on it");
            lootComponent.Item = this;
            lootComponent.Amount = dropAmount;

            rigidbody.AddForce(new Vector3(Random.Range(0f, 1f) * 10, 10f, Random.Range(0f, 1f) * 10));
        }

        #endregion
        
        public enum ItemType
        {
            Resource, Equipment
        }
    }
}
