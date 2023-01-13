using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesSelector : MonoBehaviour
    {
        [SerializeField] private InteractablesInputProcessor _inputProcessor;
        [SerializeField] private InteractablesDisplay _interactablesDisplay;

        private OrderedInteractablesDictionary _dictionary => _interactablesDisplay.DisplayedDictionary;
        private int _currentID;

        private void OnEnable()
        {
            _inputProcessor.ScrolledBack += SelectPrevious;
            _inputProcessor.ScrolledForward += SelectNext;
            _inputProcessor.Interacted += ActivateCurrent;
            
            _interactablesDisplay.IconCreated += OnIconCreated;
            _interactablesDisplay.IconRetired += OnIconRetired;
        }
        
        private void OnDisable()
        {
            _inputProcessor.ScrolledBack -= SelectPrevious;
            _inputProcessor.ScrolledForward -= SelectNext;
            _inputProcessor.Interacted -= ActivateCurrent;
            
            _interactablesDisplay.IconCreated -= OnIconCreated;
            _interactablesDisplay.IconRetired -= OnIconRetired;
        }

        private void SelectPrevious()
        {
            var current = _dictionary.GetValueByID(_currentID);
            current.gameObject.SetActive(false);
            var newElement = _dictionary.GetPreviousElement(current);
            if (current != newElement) _currentID--;
            newElement.gameObject.SetActive(true);
        }

        private void SelectNext()
        {
            var current = _dictionary.GetValueByID(_currentID);
            current.gameObject.SetActive(false);
            var newElement = _dictionary.GetNextElement(current);
            if (current != newElement) _currentID++;
            newElement.gameObject.SetActive(true);
        }

        private void ActivateCurrent()
        {
            Debug.Log(_currentID);
            var current = _dictionary.GetValueByID(_currentID);
            current.Activate();
        }

        private void OnIconRetired(InteractablePresenter presenter)
        {
            var current = _dictionary.GetValueByID(_currentID);
            if(current != presenter || _dictionary.Count == 1) return;
            
            if(_currentID != 0) SelectPrevious();
            else SelectNext();
        }
        
        private void OnIconCreated(InteractablePresenter presenter)
        {
            if(_dictionary.Count != 1) return;

            var selectedPresenter = _dictionary.GetValueByID(_currentID);
            selectedPresenter.gameObject.SetActive(true);
        }
    }
}