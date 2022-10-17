using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.HUDInput;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts
{
    public class LootPicker : MonoBehaviour
    {
        #region Serialization

        [SerializeField] private HUDInputHandler _inputHandler;
        [SerializeField] private ResourceInventory _resourceInventory;
        [SerializeField] private LootIconsSelector _lootSelector;
        
        #endregion

        private void OnEnable()
        {
            _inputHandler.LootInteracted += InstantiatePickUp;
        }

        private void OnDisable()
        {
            _inputHandler.LootInteracted -= InstantiatePickUp;
        }
        
        private void InstantiatePickUp()
        {
            var icon = _lootSelector.GetCurrentSelectedLoot();
            if (icon == null) return;
            
            PickUp(icon);
        }

        private void PickUp(LootIcon.LootIcon icon)
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
