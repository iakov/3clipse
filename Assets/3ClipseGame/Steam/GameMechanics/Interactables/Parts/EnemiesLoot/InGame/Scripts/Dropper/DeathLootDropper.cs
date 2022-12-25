using System.Collections.Generic;
using _3ClipseGame.Steam.GameCore.GlobalScripts.Pool;
using _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.Dropper
{
    public class DeathLootDropper : LootDropper
    {
        [SerializeField] private List<DropElement> _possibleDropResources;
        [SerializeField] private Pool _pool;
        [SerializeField] private GameObject _decalsParent;

        private Transform _transform;
        private ILootCreator _lootCreator;

        private void Awake()
        {
            _transform = transform;
            _lootCreator = new PooledLootCreator(_decalsParent, _pool);
        }
        
        private void OnDestroy()
            => DropAll();

        private void DropAll()
        {
            foreach (var dropElement in _possibleDropResources) 
                DropOneElement(dropElement);
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
