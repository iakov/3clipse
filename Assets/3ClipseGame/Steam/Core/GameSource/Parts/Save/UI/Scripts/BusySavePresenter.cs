using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class BusySavePresenter : SavePresenter
    {
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private TMP_Text _locationText;
        
        public event Action<GameSave> Clicked;
        public event Action<BusySavePresenter, GameSave> Cleared;

        private GameSave _trackedSave;

        public void ChangeTrackedSave(GameSave newSave)
        {
            _trackedSave = newSave;
            
            _dateText.text = newSave.SaveDate;
            _locationText.text = newSave.SaveLocation;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(_trackedSave);
        }

        public void OnClearClicked()
        {
            Cleared?.Invoke(this, _trackedSave);
        }
    }
}