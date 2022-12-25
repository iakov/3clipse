using _3ClipseGame.Steam.GameCore.GlobalScripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.Dropper
{
    public class PooledLootCreator : ILootCreator
    {
        public PooledLootCreator(GameObject decalsParent, Pool pool)
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