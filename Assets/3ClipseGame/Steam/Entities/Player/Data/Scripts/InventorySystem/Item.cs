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

        #endregion

        #region PublicMethods

        public void DropOnGround(int dropAmount, Vector3 position)
        {
            if (lootPrefab == null) throw new Exception("Object prefab is null");
            
            if (!lootPrefab.TryGetComponent<Rigidbody>(out var rigidbody)) throw new Exception("Prefab must have Rigidbody on it");
            if (!lootPrefab.TryGetComponent<PickableLoot>(out var lootComponent)) throw new Exception("Prefab must have Loot component on it");
            
            var instantiatedObject = Instantiate(lootPrefab, position, Quaternion.identity);
            rigidbody = instantiatedObject.GetComponent<Rigidbody>();
            lootComponent = instantiatedObject.GetComponent<PickableLoot>();
            
            lootComponent.Item = this;
            lootComponent.Amount = dropAmount;

            rigidbody.AddForce(new Vector3(Random.Range(0f, 1f) * 10, 100f, Random.Range(0f, 1f) * 10));
        }

        #endregion
        
        public enum ItemType
        {
            Resource, Equipment
        }
    }
}
