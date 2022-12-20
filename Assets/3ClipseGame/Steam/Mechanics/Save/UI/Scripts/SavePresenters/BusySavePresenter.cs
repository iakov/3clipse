using System;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters
{
    public class BusySavePresenter : SavePresenter
    {
        [SerializeField] private TMP_Text _dateText;
        [SerializeField] private TMP_Text _locationText;

        public event Action<BusySavePresenter> Cleared;
        
        protected override Sprite GetImage() => _trackedSave.GetImage;

        public GameSave TrackedSave => _trackedSave;
        private GameSave _trackedSave;

        public override void Use() => InterSceneSavesEntry.LoadSave(_trackedSave.ID);
        
        public void OnClearClicked() => Cleared?.Invoke(this);

        public void ChangeTrackedSave(GameSave newSave)
        {
            _trackedSave = newSave;
            
            _dateText.text = newSave.SaveDate;
            _locationText.text = newSave.SaveName;
        }
    }
}