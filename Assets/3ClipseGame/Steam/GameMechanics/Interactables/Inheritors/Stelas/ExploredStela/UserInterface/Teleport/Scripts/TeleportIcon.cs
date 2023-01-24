using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.UserInterface
{
    public class TeleportIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _difficulty;
        [SerializeField] private Image _image;
        
        public void SetName(string teleportName) => _nameText.text = teleportName;
        public void SetDifficulty(int difficulty) => _difficulty.text = new string('*', difficulty);
        public void SetImage(Sprite imageSprite) => _image.sprite = imageSprite;
    }
}