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
        public event Action IconRetired;
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
            if(DisplayedDictionary.Contains(interactable)) return;
            
            interactable.Disappeared += DestructPresenter;
            StructPresenter(interactable);
        }

        private void StructPresenter(Interactable interactable)
        {
            var presenterInstance = CreatePresenter(interactable);
            DisplayedDictionary.AddElement(interactable, presenterInstance);
            IconCreated?.Invoke(presenterInstance);
        }

        private InteractablePresenter CreatePresenter(Interactable interactable)
        {
            var detectedPresenterPrefab = interactable.GetNewPresenter();
            var presenterInstance = Instantiate(detectedPresenterPrefab, _iconsParent);
            presenterInstance.ChangeInteractable(interactable);
            return presenterInstance;
        }
        
        private void RemoveInteractable(Interactable interactable)
        {
            if(DisplayedDictionary.Contains(interactable) == false) return;
            
            interactable.Disappeared -= DestructPresenter;
            DestructPresenter(interactable);
        }

        private void DestructPresenter(Interactable interactable)
        {
            var presenter = DisplayedDictionary.GetValueByKey(interactable);
            
            IconRetiring?.Invoke(presenter);
            DeletePresenter(interactable, presenter);
            IconRetired?.Invoke();
        }
        
        private void DeletePresenter(Interactable interactable, InteractablePresenter presenter)
        {
            DisplayedDictionary.RemoveElement(interactable);
            Destroy(presenter.gameObject);
        }
    }
}