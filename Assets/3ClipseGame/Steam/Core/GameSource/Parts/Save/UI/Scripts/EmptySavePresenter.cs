using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Visuals;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class EmptySavePresenter : SavePresenter
    {
        public event Action<EmptySavePresenter> Clicked;

        public override void Use()
        {
            SaveManager.Instance.NewGame();
        }

        public override void Select()
        {
            _selectedHighlightObject.SetActive(true);
            IsSelected = true;
            Unhighlight();
        }

        public override void Unselect()
        {
            var highlightGameObject = _selectedHighlightObject.gameObject;
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

        public override void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
    }
}