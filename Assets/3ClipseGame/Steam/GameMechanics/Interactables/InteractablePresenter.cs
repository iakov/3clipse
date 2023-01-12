using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables
{
    public abstract class InteractablePresenter : MonoBehaviour
    {
        private Interactable _currentInteractable;

        public void ChangeInteractable(Interactable newInteractable) => _currentInteractable = newInteractable;
        public void Activate() => _currentInteractable.Activate();
    }
}