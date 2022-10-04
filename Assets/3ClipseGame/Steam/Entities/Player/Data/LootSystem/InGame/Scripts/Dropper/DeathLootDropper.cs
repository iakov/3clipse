using System.Collections.Generic;
using _3ClipseGame.Steam.Core.Scripts.Pool;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Dropper
{
    public class DeathLootDropper : LootDropper
    {
        #region Serialization

        [SerializeField] private List<DropElement> _possibleDropResources;
        [SerializeField] private Pool _pool;
        [SerializeField] private GameObject _decalsParent;

        #endregion

        #region Initialization

        private Transform _transform;
        private ILootCreator _lootCreator;

        private void Awake()
        {
            _transform = transform;
            _lootCreator = new PooledLootCreator(_decalsParent, _pool);
        }

        #endregion
        
        private void OnDestroy()
        {
            DropAll();
        }

        private void DropAll()
        {
            foreach (var dropElement in _possibleDropResources)
            {
                DropOneElement(dropElement);
            }
        }
        
        private void DropOneElement(DropElement element)
        {
            var pickableLootObject = _lootCreator?.CreateLootObjectInPosition(_transform);
            var pickableLootComponent = GetPickableLootFromObject(pickableLootObject);
            
            SetPickableLootTrack(pickableLootComponent, element);
            DropLoot(pickableLootComponent);
        }
        
        private void SetPickableLootTrack(PickableLoot loot, DropElement element)
        {
            loot.SetDropElement(element);
        }

        private PickableLoot GetPickableLootFromObject(GameObject objectToGetFrom)
        {
            return objectToGetFrom.GetComponent<PickableLoot>();
        }

        private void DropLoot(PickableLoot loot)
        {
            loot.GetResource().Instantiate(loot.gameObject);
        }
    }
}
