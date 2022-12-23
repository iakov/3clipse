using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.HUDInput;
using _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using _3ClipseGame.Steam.GameMechanics.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.LootSystem.UI.Scripts
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
