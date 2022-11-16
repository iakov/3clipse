using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.HUDInput;
using _3ClipseGame.Steam.Mechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.LootSystem.UI.Scripts
{
    public class LootPicker : MonoBehaviour
    {
        [SerializeField] private HUDInputHandler _inputHandler;
        [SerializeField] private ResourceInventory _resourceInventory;
        [SerializeField] private LootIconsSelector _lootSelector;

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

        private void PickUp(Entities.Player.Data.LootSystem.UI.Scripts.LootIcon.LootIcon icon)
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
