using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects.Resources.Item
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resources/Items/Resource")]
    public class Resource : InventorySystem.ScriptableObjects.Item
    {
        [SerializeField] private int maximumAmountInSlot = 99;

        public int MaximumAmountInSlot => maximumAmountInSlot;
    }
}
