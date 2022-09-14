using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts.LootIconsListControls
{
    public class LootHighlighter : MonoBehaviour
    {
        private LootIconsSelector _lootIconsSelector;

        private void Awake()
        {
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

        private void OnSelectedLootChanged(LootIcon previousLoot, LootIcon newLoot)
        {
            SwitchHighlightedIcon(previousLoot, newLoot);
        }

        private void SwitchHighlightedIcon(LootIcon previousLoot, LootIcon newLoot)
        {
            previousLoot !? .SetActive(false);
            newLoot !? .SetActive(true);
        }
    }
}