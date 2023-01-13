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
            _interactablesDisplay.IconCreated += OnIconCreated;
            _interactablesDisplay.IconRetiring += OnIconRetiring;
            
            _inputProcessor.ScrolledForward += SelectNextIcon;
            _inputProcessor.ScrolledBack += SelectPreviousIcon;
            _inputProcessor.Interacted += ActivateCurrent;
        }
        
        private void OnDisable()
        {
            _interactablesDisplay.IconCreated -= OnIconCreated;
            _interactablesDisplay.IconRetiring -= OnIconRetiring;
            
            _inputProcessor.ScrolledForward -= SelectNextIcon;
            _inputProcessor.ScrolledBack -= SelectPreviousIcon;
            _inputProcessor.Interacted -= ActivateCurrent;
        }
        
        private void OnIconCreated(InteractablePresenter presenter)
        {
            if(_dictionary.Count != 1) return;
            presenter.gameObject.SetActive(true);
        }
        
        private void OnIconRetiring(InteractablePresenter presenter)
        {
            var current = _dictionary.GetValueByID(_currentID);
            if(current != presenter) return;
            
            DeleteIcon(current);
        }

        private void DeleteIcon(InteractablePresenter current)
        {
            _dictionary.RemoveElement(_currentID);
            if (_dictionary.Count == 0) return;
            SelectNext(current);
        }
        
        private void SelectNextIcon()
        {
            var current = _dictionary.GetValueByID(_currentID);
            SelectNext(current);
        }

        private void SelectNext(InteractablePresenter current)
        {
            if(current == null) return;
            
            current.gameObject.SetActive(false);
            var newElement = _dictionary.GetNextElement(current);
            newElement.gameObject.SetActive(true);
            
            _currentID = _dictionary.GetIDByValue(newElement);
        }

        private void SelectPreviousIcon()
        {
            var current = _dictionary.GetValueByID(_currentID);
            SelectPrevious(current);
        }

        private void SelectPrevious(InteractablePresenter current)
        {
            if(current == null) return;
            
            current.gameObject.SetActive(false);
            var newElement = _dictionary.GetPreviousElement(current);
            newElement.gameObject.SetActive(true);

            _currentID = _dictionary.GetIDByValue(newElement);
        }

        private void ActivateCurrent()
        {
            if(_dictionary.Count == 0) return;
            
            var current = _dictionary.GetValueByID(_currentID);
            current.Activate();
        }
    }
}