using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resources/Items/Resource")]
    public class Resource : Item
    {
        #region SerializeFields

        [SerializeField] private int maximumAmountInSlot = 99;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => TypeOfItem = ItemType.Resource;

        #endregion

        #region PublicGetters

        public int MaximumAmountInSlot => maximumAmountInSlot;

        #endregion
    }
}
