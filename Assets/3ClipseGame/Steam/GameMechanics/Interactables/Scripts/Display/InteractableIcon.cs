using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Scripts.Display
{
    public abstract class InteractableIcon : MonoBehaviour
    {
        public abstract InteractableIcon Initialize(Interactable interactable);
        public abstract Interactable GetCurrentInteractable();
        public abstract void ChangeInteractable(Interactable interactable);
        public abstract void SetSelected(bool isSelected);
    }
}