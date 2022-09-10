using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts.LootIconsListControls
{
    public class LootHighlighter : MonoBehaviour
    {
        private LootDisplay _lootDisplay;
        private LootIconsSelector _lootIconsSelector;

        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootIconsSelector = GetComponent<LootIconsSelector>();
        }

        private void OnEnable()
        {
            _lootIconsSelector.SelectedLootChanged += OnSelectedLootChanged;
        }

        private void OnDisable()
        {
            _lootIconsSelector.SelectedLootChanged -= OnSelectedLootChanged;
        }

        private void OnSelectedLootChanged(PickableLoot previousLoot)
        {
            _lootDisplay.GetIconByObject(previousLoot)?.SetActive(false);
            var currentLoot = _lootIconsSelector.CurrentSelectedLoot;
            _lootDisplay.GetIconByObject(currentLoot).SetActive(true);
        }
    }
}