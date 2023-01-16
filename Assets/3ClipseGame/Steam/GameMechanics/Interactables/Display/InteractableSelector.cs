using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesSelector : MonoBehaviour
    {
        [SerializeField] private InteractablesInputProcessor _inputProcessor;
        [SerializeField] private InteractablesDisplay _interactablesDisplay;

        private InteractablePresenter _currentPresenter;

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
            if (_interactablesDisplay.DisplayedDictionary.Count == 1) _currentPresenter = presenter;
            else presenter.gameObject.SetActive(false);
        }
        
        private void OnIconRetiring(InteractablePresenter presenter)
        {
            if(presenter != _currentPresenter) return;
            
            if (_interactablesDisplay.DisplayedDictionary.Count == 1) _currentPresenter = null;
            else SelectNextIcon();
        }
        
        private void SelectNextIcon()
        {
            SelectNext(_currentPresenter);
        }

        private void SelectNext(InteractablePresenter current)
        {
            if(_currentPresenter == null) return;
            
            current.gameObject.SetActive(false);
            _currentPresenter = _interactablesDisplay.DisplayedDictionary.GetNextElement(current);
            _currentPresenter.gameObject.SetActive(true);
        }

        private void SelectPreviousIcon()
        {
            SelectPrevious(_currentPresenter);
        }

        private void SelectPrevious(InteractablePresenter current)
        {
            if(_currentPresenter == null) return;
            
            current.gameObject.SetActive(false);
            _currentPresenter = _interactablesDisplay.DisplayedDictionary.GetPreviousElement(current);
            _currentPresenter.gameObject.SetActive(true);
        }

        private void ActivateCurrent()
        {
            if (_currentPresenter == null) return;

            var deletingIcon = _currentPresenter;
            SelectNextIcon();
            deletingIcon.Activate();
        }
    }
}