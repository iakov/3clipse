using System.Collections;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using _3ClipseGame.Steam.Global.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Dropper
{
    public class DeathLootDropper : MonoBehaviour
    {
        [SerializeField] private List<DropElement> _possibleDropResources;
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private GameObject _decalsParent;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;   
        }
        
        public IEnumerator Drop(float dropRate = 0.05f)
        {
            var lootParent = new GameObject("Death Loot");
            lootParent.transform.parent = _decalsParent.transform;
            
            foreach (var dropElement in _possibleDropResources)
            {
                var dropAmount = dropElement.GetFinalAmountOfDrop();
                if (dropAmount == 0) yield return new WaitForSeconds(0);

                var poolObject = _objectPool.GetPoolObject();
                
                var pickableLootComponent = poolObject.GetComponent<PickableLoot>();
                if (!pickableLootComponent) pickableLootComponent = poolObject.AddComponent<PickableLoot>();
                
                pickableLootComponent.Amount = dropAmount;
                pickableLootComponent.Resource = dropElement.DropItem;

                var lootTransform = pickableLootComponent.transform;
                lootTransform.parent = lootParent.transform;
                lootTransform.position = _transform.position + Vector3.up;

                pickableLootComponent.Resource.Instantiate(pickableLootComponent);

                yield return new WaitForSeconds(dropRate);
            }
        }
    }
}
