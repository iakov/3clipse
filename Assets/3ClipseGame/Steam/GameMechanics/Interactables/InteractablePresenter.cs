using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables
{
    public abstract class InteractablePresenter : MonoBehaviour
    {
        protected Interactable CurrentInteractable;

        public void ChangeInteractable(Interactable newInteractable) => CurrentInteractable = newInteractable;

        public abstract void Activate();
    }
}