using System;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts
{
    public class EmptySavePresenter : SavePresenter
    {
        [SerializeField] private RectTransform _hoverHighlight;

        public override GameSave TrackedSave
        {
            get => null; 
            set{}
        }
        
        public override event Action<SavePresenter> Clicked;

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
    }
}