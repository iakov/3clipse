using System.Collections;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using _3ClipseGame.Steam.Global.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Scripts
{
    public class DeathLootDropper : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private List<DropElement> possibleDropResources;
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private GameObject decalsParent;

        #endregion

        #region PrivateFields

        private Transform _transform;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => _transform = transform;

        #endregion
        
        #region PublicMethods

        public IEnumerator Drop(float dropRate = 0.05f)
        {
            var lootParent = new GameObject("Death Loot");
            lootParent.transform.parent = decalsParent.transform;
            
            foreach (var dropElement in possibleDropResources)
            {
                var dropAmount = dropElement.GetFinalAmountOfDrop();
                if (dropAmount == 0) yield return new WaitForSeconds(0);

                var poolObject = objectPool.GetPoolObject();
                
                var pickableLootComponent = poolObject.GetComponent<PickableLoot>();
                if (!pickableLootComponent) pickableLootComponent = poolObject.AddComponent<PickableLoot>();
                
                pickableLootComponent.Amount = dropAmount;
                pickableLootComponent.Resource = dropElement.dropItem;

                var lootTransform = pickableLootComponent.transform;
                lootTransform.parent = lootParent.transform;
                lootTransform.position = _transform.position + Vector3.up;

                pickableLootComponent.Resource.Instantiate(pickableLootComponent);

                yield return new WaitForSeconds(dropRate);
            }
        }

        #endregion
    }
}
