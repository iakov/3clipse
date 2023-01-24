using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.UserInterface
{
    [CreateAssetMenu(fileName = "New Teleport Data", menuName = "Map/Teleport Icon")]
    public class TeleportData : ScriptableObject
    {
        [SerializeField] private string _teleportName;
        [SerializeField] private int _dangerLevel;
        [SerializeField] private Sprite _teleportIcon;

        public void ApplyDataToIcon(TeleportIcon icon)
        {
            icon.SetName(_teleportName);
            icon.SetDifficulty(_dangerLevel);
            icon.SetImage(_teleportIcon);
        }
    }
}
