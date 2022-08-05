using System.Collections;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class DeathLoot : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private List<DropElement> possibleDropResources;
        [SerializeField] private ObjectPool objectPool;

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
            var lootParent = new GameObject("DeathDrop");
            lootParent.transform.parent = _transform.parent;
            
            foreach (var dropElement in possibleDropResources)
            {
                var dropAmount = dropElement.GetFinalAmountOfDrop();
                
                if(dropAmount > 0) dropElement.dropItem.DropOnGround(objectPool.GetPoolObject(), dropAmount, _transform.position, lootParent.transform);
                yield return new WaitForSeconds(dropAmount > 0 ? dropRate : 0);
            }
        }

        #endregion
    }
}
