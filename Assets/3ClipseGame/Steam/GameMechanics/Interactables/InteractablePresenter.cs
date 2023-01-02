using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables
{
    public abstract class InteractablePresenter : MonoBehaviour
    {
         private Interactable<InteractablePresenter> _currentInteractable;

         public void ChangeInteractable(Interactable<InteractablePresenter> interactable) =>
             _currentInteractable = interactable;

        public void Activate() => _currentInteractable.Activate();
    }
}