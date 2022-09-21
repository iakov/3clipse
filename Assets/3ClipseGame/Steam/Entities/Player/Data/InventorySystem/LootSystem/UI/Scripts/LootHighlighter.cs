namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts
{
    public class LootHighlighter
    {
        #region Public

        public void SwitchHighlightedIcon(LootIcon previousLoot, LootIcon newLoot)
        {
            previousLoot !? .SetActive(false);
            newLoot !? .SetActive(true);
        }

        #endregion
    }
}