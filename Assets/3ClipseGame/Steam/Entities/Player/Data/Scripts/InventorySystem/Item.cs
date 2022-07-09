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

        #region PrivateFields

        private float dropStrength = 100f;

        #endregion

        #region PublicMethods

        public void DropOnGround(Vector3 position, int amount)
        {
            if (lootPrefab == null) throw new Exception("Object prefab is null");
            
            if (!lootPrefab.GetComponent<Rigidbody>()) throw new Exception("Prefab must have Rigidbody on it");
            
            var instantiatedObject = Instantiate(lootPrefab, position, Quaternion.identity);
            var rigidbody = instantiatedObject.GetComponent<Rigidbody>();

            if (!instantiatedObject.TryGetComponent<PickableLoot>(out var lootComponent)) lootComponent = instantiatedObject.AddComponent<PickableLoot>();
            lootComponent.Resource = this;
            lootComponent.Amount = amount;

            rigidbody.AddForce(new Vector3(Random.Range(0f, 1f) * dropStrength, dropStrength, Random.Range(0f, 1f) * dropStrength));
            rigidbody.freezeRotation = true;
        }

        #endregion
    }
}
