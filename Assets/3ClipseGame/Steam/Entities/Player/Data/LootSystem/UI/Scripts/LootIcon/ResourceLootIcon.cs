using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.UI.Scripts.LootIcon
{
    public class ResourceLootIcon : LootIcon
    {
        #region Public

        public override PickableLoot GetCurrentLoot()
        {
            return _displayableLoot;
        }
        
        public override void SetHighlight(bool isActive)
        {
            if(_highlight != null) 
                _highlight.gameObject.SetActive(isActive);
        }

        public override void SwitchTrack(PickableLoot newLoot)
        {
            UnbindCurrentTrack();
            _displayableLoot = newLoot;
            UpdateView();
            BindCurrentTrack();
        }

        public override bool IsHighlighted()
        {
            return _highlight.gameObject.activeInHierarchy;
        }

        #endregion
        
        #region Serialization

        [SerializeField] private RectTransform _highlight;
        [SerializeField] private Image _imageComponent;
        [SerializeField] private Text _textComponent;

        #endregion

        #region Initialization

        private PickableLoot _displayableLoot;

        #endregion

        private void UnbindCurrentTrack()
        {
            if(_displayableLoot != null)
                _displayableLoot.TrackedElementUpdated -= UpdateView;
        }

        private void BindCurrentTrack()
        {
            if(_displayableLoot != null) 
                _displayableLoot.TrackedElementUpdated += UpdateView;
        }

        private void UpdateView()
        {
            if (_displayableLoot == null || _displayableLoot.GetResource() == null) return;
            
            _imageComponent.sprite = _displayableLoot.GetResource().UIImage;
            _textComponent.text = "x" + _displayableLoot.GetAmount();
        }
    }
}