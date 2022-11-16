using System;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts
{
    public class BusySavePresenter : SavePresenter
    {
        [SerializeField] private RectTransform _hoverHighlight;
        [SerializeField] private Image _saveImage;
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private TMP_Text _locationNameText;

        public override event Action<SavePresenter> Clicked;
        public event Action<BusySavePresenter> Cleared;

        public override GameSave TrackedSave
        {
            get => _trackedSave;
            set => UpdateSave(value);
        }

        private GameSave _trackedSave;

        private void UpdateSave(GameSave value)
        {
            _trackedSave = value;
            
            _saveImage.sprite = _trackedSave.Image;
            _dateText.text = _trackedSave.Date;
            _locationNameText.text = _trackedSave.LocationName;
        }

        public override void OnPointerEnter(PointerEventData data)
        {
            _hoverHighlight.gameObject.SetActive(true);
        }

        public override void OnPointerExit(PointerEventData data)
        {
            _hoverHighlight.gameObject.SetActive(false);
        }

        public override void OnPointerClick(PointerEventData data)
        {
            Clicked?.Invoke(this);
        }

        public void Clear()
        {
            Cleared?.Invoke(this);
        }
    }
}