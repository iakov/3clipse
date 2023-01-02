using System.Collections.Generic;
using _3ClipseGame.Steam.GameMechanics.Interactables.Detector;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesDisplay : MonoBehaviour
    {
        [SerializeField] private InteractablesDetector _interactablesHolder;
        [SerializeField] private Transform _iconsParent;

        private Dictionary<Interactable<InteractablePresenter>, InteractablePresenter> _displayedDictionary = new();

        private void OnEnable()
        {
            _interactablesHolder.InteractableDetected += DisplayNewInteractable;
            _interactablesHolder.InteractableRetired += RemoveInteractable;
        }
        
        private void OnDisable()
        {
            _interactablesHolder.InteractableDetected -= DisplayNewInteractable;
            _interactablesHolder.InteractableRetired -= RemoveInteractable;
        }

        private void DisplayNewInteractable(Interactable<InteractablePresenter> interactable)
        {
            var originalPresenter = interactable.GetPresenter();
            var presenter = Instantiate(originalPresenter, Vector3.zero, Quaternion.identity, _iconsParent);
            _displayedDictionary.Add(interactable, presenter);
            presenter.ChangeInteractable(interactable);
        }
        
        private void RemoveInteractable(Interactable<InteractablePresenter> interactable)
        {
            if (_displayedDictionary.ContainsKey(interactable) == false) return;
            var presenter = _displayedDictionary[interactable];

            _displayedDictionary.Remove(interactable);
            Destroy(presenter.gameObject);
        }
    }
}