using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts
{
    public class LootIcon : MonoBehaviour
    {
        #region Serialization

        [SerializeField] private RectTransform _highlight;
        [SerializeField] private Image _imageComponent;
        [SerializeField] private Text _textComponent;

        #endregion

        #region Initialization

        private PickableLoot _displayableLoot;

        #endregion
        
        #region Public

        public PickableLoot GetCurrentLoot()
        {
            return _displayableLoot;
        }
        
        public void SetActive(bool isActive)
        {
            _highlight.gameObject.SetActive(isActive);
        }

        public void SwitchTrack(PickableLoot newLoot)
        {
            UnbindCurrentTrack();
            _displayableLoot = newLoot;
            UpdateView();
            BindCurrentTrack();
        }

        #endregion

        private void UnbindCurrentTrack()
        {
            if(_displayableLoot == null) return;
            
            _displayableLoot.TrackedElementUpdated -= UpdateView;
        }

        private void BindCurrentTrack()
        {
            _displayableLoot.TrackedElementUpdated += UpdateView;
        }

        private void UpdateView()
        {
            _imageComponent.sprite = _displayableLoot.GetResource().UIImage;
            _textComponent.text = "x" + _displayableLoot.GetAmount();
        }
    }
}
