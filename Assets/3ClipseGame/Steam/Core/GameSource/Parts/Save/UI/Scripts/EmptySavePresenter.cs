using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame;
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

        public override void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
    }
}