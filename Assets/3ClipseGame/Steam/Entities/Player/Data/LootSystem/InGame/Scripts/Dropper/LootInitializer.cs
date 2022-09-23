using _3ClipseGame.Steam.Global.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.Dropper
{
    public class LootInitializer : ILootCreator
    {
        public LootInitializer(GameObject decalsParent, Pool pool)
        {
            _pool = pool;
            _decalsParent = decalsParent;
            _deathLootParent = InstantiateLootParent();
        }
        
        private GameObject InstantiateLootParent()
        {
            var lootParent = new GameObject("Death Loot");
            lootParent.transform.parent = _decalsParent.transform;
            
            return lootParent;
        }

        private GameObject _decalsParent;
        private Pool _pool;
        private GameObject _deathLootParent;

        public GameObject CreateLootObjectInPosition(Transform positionTransform)
        {
            return CreateObject(positionTransform.position);
        }

        private GameObject CreateObject(Vector3 position)
        {
            var poolObject = _pool.GetPoolObject();
            poolObject.transform.parent = _deathLootParent.transform;
            poolObject.transform.position = position;
            return poolObject;
        }
    }
}