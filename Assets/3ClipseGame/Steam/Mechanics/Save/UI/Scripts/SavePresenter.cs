using System;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts
{
    public class SavePresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action<SavePresenter> Selected;
        public GameSave TrackedGameSave { get; private set; }

        [Header("Components")]
        [SerializeField] private Image _saveImage;
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private TMP_Text _locationNameText;
        [SerializeField] private RectTransform _selectedHighlight;
        
        [Header("Hover")]
        [SerializeField] private float _hoverScale;
        [SerializeField] private float _hoverTime;

        private Sprite _saveScreenshot;
        private bool _isSelected;

        public void ChangeTrackedGameSave(GameSave save)
        {
            TrackedGameSave = save;

            _dateText.text = save.Date;
            _locationNameText.text = save.Name;
            _saveImage.sprite = save.Image;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!_isSelected)
                LeanTween.scale(gameObject, Vector3.one * _hoverScale, _hoverTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, Vector3.one, _hoverTime);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            LeanTween.scale(gameObject, Vector3.one, _hoverTime);
            Selected?.Invoke(this);
        }

        public void SetSelected(bool isSelected)
        {
            _selectedHighlight.gameObject.SetActive(isSelected);
            _isSelected = isSelected;
        }
    }
}
