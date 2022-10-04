using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts.LootIcon
{
    public abstract class LootIcon : MonoBehaviour
    {
        public abstract PickableLoot GetCurrentLoot();
        public abstract void SetHighlight(bool isActive);
        public abstract void SwitchTrack(PickableLoot newLoot);
        public abstract bool IsHighlighted();
    }
}
