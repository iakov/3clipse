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
        }
        
        private void OnDisable()
        {
            _inputProcessor.ScrolledBack -= SelectPrevious;
            _inputProcessor.ScrolledForward -= SelectNext;
            _inputProcessor.Interacted -= ActivateCurrent;
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
    }
}