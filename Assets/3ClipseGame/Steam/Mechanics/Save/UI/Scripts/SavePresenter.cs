using System;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts
{
    public abstract class SavePresenter : MonoBehaviour,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public abstract GameSave TrackedSave { get; set; }
        
        public abstract event Action<SavePresenter> Clicked;
        
        public abstract void OnPointerEnter(PointerEventData data);

        public abstract void OnPointerExit(PointerEventData data);

        public abstract void OnPointerClick(PointerEventData data);
    }
}