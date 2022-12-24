namespace _3ClipseGame.Steam.GameMechanics.LootSystem.UI.Scripts
{
    public class LootHighlighter
    {
        #region Public

        public void SwitchHighlightedIcon(LootIcon.LootIcon previousLoot, LootIcon.LootIcon currentLoot)
        {
            previousLoot !? .SetHighlight(false);
            currentLoot !? .SetHighlight(true);
        }

        #endregion
    }
}