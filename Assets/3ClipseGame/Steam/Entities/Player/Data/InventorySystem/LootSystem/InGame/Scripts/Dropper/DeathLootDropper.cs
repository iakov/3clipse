using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent;
using _3ClipseGame.Steam.Entities.Scripts;
using _3ClipseGame.Steam.Global.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.Dropper
{
    [RequireComponent(typeof(Entity))]
    
    public class DeathLootDropper : MonoBehaviour
    {
        #region Serialization

        [SerializeField] private List<DropElement> _possibleDropResources;
        [SerializeField] private Pool pool;
        [SerializeField] private GameObject _decalsParent;

        #endregion

        #region Initialization

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        #endregion

        #region Drop

        private void OnDisable()
        {
            Drop();
        }

        private void Drop()
        {
            var lootParent = InstantiateLootParent();

            foreach (var dropElement in _possibleDropResources)
            {
                DropOneElement(dropElement, lootParent);
            }
        }

        private GameObject InstantiateLootParent()
        {
            var lootParent = new GameObject("Death Loot");
            lootParent.transform.parent = _decalsParent.transform;
            
            return lootParent;
        }

        private void DropOneElement(DropElement element, GameObject lootParent)
        {
            var pickableLoot = InstantiateObjectWithPickableLootComponent(lootParent);
            
            SetPickableLootTrack(pickableLoot, element);
            DropLoot(pickableLoot);
        }

        private PickableLoot InstantiateObjectWithPickableLootComponent(GameObject lootParent)
        {
            var poolObject = pool.GetPoolObject();
            poolObject.transform.parent = lootParent.transform;
            poolObject.transform.position = _transform.position;
            
            return GetPickableLootFromObject(poolObject);
        }

        private PickableLoot GetPickableLootFromObject(GameObject objectToGetFrom)
        {
            return objectToGetFrom.GetComponent<PickableLoot>();
        }

        private void SetPickableLootTrack(PickableLoot loot, DropElement element)
        {
            loot.SetDropElement(element);
        }

        private void DropLoot(PickableLoot loot)
        {
            loot.GetResource().Instantiate(loot.gameObject);
        }

        #endregion
    }
}
