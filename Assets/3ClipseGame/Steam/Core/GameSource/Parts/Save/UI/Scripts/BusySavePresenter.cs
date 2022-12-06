using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Visuals;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class BusySavePresenter : SavePresenter
    {
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private TMP_Text _locationText;

        public event Action<BusySavePresenter> Clicked;
        public event Action<BusySavePresenter> Cleared;

        public GameSave TrackedSave => _trackedSave;
        private GameSave _trackedSave;

        public override void Use()
        {
            SaveManager.Instance.LoadGame(_trackedSave.ID);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
        
        public void ChangeTrackedSave(GameSave newSave)
        {
            _trackedSave = newSave;
            
            _dateText.text = newSave.SaveDate;
            _locationText.text = newSave.SaveLocation;
        }

        public override void Select()
        {
            _selectedHighlightObject.SetActive(true);
            IsSelected = true;
        }
        
        public override void Unselect()
        {
            var highlightGameObject = _selectedHighlightObject;
            var unscaleSlowly = highlightGameObject.GetComponent<ScaleUpOnEnable>();
            unscaleSlowly.ScaleDown();
            IsSelected = false;
        }

        protected override void Highlight()
        {
            if(IsSelected) return;
            var highlightGameObject = _hoverHighlightObject.gameObject;
            highlightGameObject.SetActive(true);
        }

        protected override void Unhighlight()
        {
            var highlightGameObject = _hoverHighlightObject.gameObject;
            var unscaleSlowly = highlightGameObject.GetComponent<ScaleUpOnEnable>();
            unscaleSlowly.ScaleDown();
        }

        public void OnClearClicked()
        {
            Cleared?.Invoke(this);
        }
    }
}