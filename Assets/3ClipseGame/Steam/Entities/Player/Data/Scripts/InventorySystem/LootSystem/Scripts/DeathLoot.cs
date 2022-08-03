using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class DeathLoot : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private List<DropElement> possibleDropResources;

        #endregion

        #region PublicMethods

        public IEnumerator Drop(float dropRate = 0.05f)
        {
            foreach (var dropElement in possibleDropResources)
            {
                dropElement.dropItem.DropOnGround(transform.position, dropElement.GetFinalAmountOfDrop());
                yield return new WaitForSeconds(dropRate);
            }
            
            yield break;
        }

        #endregion
    }
}
