namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts
{
    public class LootHighlighter
    {
        #region Initialization

        public LootHighlighter(LootIconsSelector lootIconsSelector)
        {
            _lootIconsSelector = lootIconsSelector;
        }

        private LootIconsSelector _lootIconsSelector;

        #endregion

        #region Public

        public void SwitchHighlightedIcon(LootIcon previousLoot, LootIcon newLoot)
        {
            previousLoot !? .SetActive(false);
            newLoot !? .SetActive(true);
        }

        #endregion
    }
}