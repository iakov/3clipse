using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class DeathLoot : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private List<DropElement> possibleDropResources;

        #endregion

        #region MonoBehaviourMethods

        private void OnDestroy()
        {
            foreach (var dropElement in possibleDropResources)
                dropElement.dropItem.DropOnGround(transform.position, dropElement.GetFinalAmountOfDrop());
        }

        #endregion
    }
}
