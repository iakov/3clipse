using System;
using _3ClipseGame.Steam.GameMechanics.Interactables.Detector;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesDisplay : MonoBehaviour
    {
        [SerializeField] private DetectedInteractablesHolder _interactablesHolder;
        [SerializeField] private Transform _iconsParent;

        public event Action<InteractablePresenter> IconRetiring;
        public event Action<InteractablePresenter> IconCreated;

        public readonly OrderedInteractablesDictionary DisplayedDictionary = new();

        private void OnEnable()
        {
            _interactablesHolder.InteractableAdded += DisplayNewInteractable;
            _interactablesHolder.InteractableRemoved += RemoveInteractable;
        }
        
        private void OnDisable()
        {
            _interactablesHolder.InteractableAdded -= DisplayNewInteractable;
            _interactablesHolder.InteractableRemoved -= RemoveInteractable;
        }

        private void DisplayNewInteractable(Interactable interactable)
        {
            var presenter = CreatePresenter(interactable);
            DisplayedDictionary.AddElement(interactable, presenter);
            interactable.Disappeared += OnDisappear;
            IconCreated?.Invoke(presenter);
        }

        private InteractablePresenter CreatePresenter(Interactable interactable)
        {
            var originalPresenter = interactable.GetNewPresenter();
            var presenter = Instantiate(originalPresenter, _iconsParent);
            presenter.ChangeInteractable(interactable);
            presenter.gameObject.SetActive(false);
            return presenter;
        }
        
        private void RemoveInteractable(Interactable interactable)
        {
            if (DisplayedDictionary.Contains(interactable) == false) return;
            
            interactable.Disappeared -= OnDisappear;
            var presenter = DisplayedDictionary.GetValueByKey(interactable);
            DestroyInteractable(interactable, presenter);
        }

        private void OnDisappear(Interactable interactable)
        {
            interactable.Disappeared -= OnDisappear;
            var presenter = DisplayedDictionary.GetValueByKey(interactable);
            DestroyInteractable(interactable, presenter);
        }
        
        private void DestroyInteractable(Interactable interactable, InteractablePresenter presenter)
        {
            IconRetiring?.Invoke(presenter);
            Destroy(presenter.gameObject);
        }
    }
}