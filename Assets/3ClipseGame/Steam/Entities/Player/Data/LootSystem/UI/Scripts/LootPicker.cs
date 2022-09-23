using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts
{
    public class LootPicker : MonoBehaviour
    {
        #region Serialization

        [SerializeField] private InputAction _pickUpAction;
        [SerializeField] private ResourceInventory _resourceInventory;
        [SerializeField] private LootIconsSelector _lootSelector;
        
        #endregion

        #region Initialization

        private void Awake()
        {
            _pickUpAction.Enable();
        }

        #endregion

        private void OnEnable()
        {
            _pickUpAction.started += InstantiatePickUp;
        }

        private void OnDisable()
        {
            _pickUpAction.started -= InstantiatePickUp;
        }
        
        private void InstantiatePickUp(InputAction.CallbackContext context)
        {
            var icon = _lootSelector.GetCurrentSelectedLoot();
            if (icon == null) return;
            
            PickUp(icon);
        }

        private void PickUp(LootIcon icon)
        {
            var loot = icon.GetCurrentLoot();
            AddItemToStorage(loot);
            DeleteLoot(loot);
        }
        
        private void AddItemToStorage(PickableLoot loot)
        {
            _resourceInventory.TryAddItem(loot.GetResource(), loot.GetAmount());
        }

        private void DeleteLoot(PickableLoot loot)
        {
            loot.Disappear();
        }
    }
}
