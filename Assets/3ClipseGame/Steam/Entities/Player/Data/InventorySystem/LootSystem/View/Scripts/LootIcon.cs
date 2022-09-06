using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootIcon : MonoBehaviour
    {
        [SerializeField] private RectTransform _highlight;
        [SerializeField] private Image _imageComponent;
        [SerializeField] private Text _textComponent;

        private PickableLoot _displayableLoot;
        
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

        private void UnbindCurrentTrack()
        {
            if(_displayableLoot == null) return;
            
            _displayableLoot.AmountChanged -= UpdateView;
            _displayableLoot.ResourceChanged -= UpdateView;
        }

        private void BindCurrentTrack()
        {
            _displayableLoot.AmountChanged += UpdateView;
            _displayableLoot.ResourceChanged += UpdateView;
        }

        private void UpdateView()
        {
            _imageComponent.sprite = _displayableLoot.Resource.UIImage;
            _textComponent.text = "x" + _displayableLoot.Amount.ToString();
        }
    }
}
