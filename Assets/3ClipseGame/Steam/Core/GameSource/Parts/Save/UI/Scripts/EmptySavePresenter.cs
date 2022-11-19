using System;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class EmptySavePresenter : SavePresenter
    {
        public event Action<EmptySavePresenter> Clicked;

        public override void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
    }
}