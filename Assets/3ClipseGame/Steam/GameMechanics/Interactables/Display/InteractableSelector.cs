using _3ClipseGame.Steam.GameMechanics.Interactables.Detector;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesSelector : MonoBehaviour
    {
        [SerializeField] private InteractablesInputProcessor _inputProcessor;
        [SerializeField] private DetectedInteractablesHolder _interactablesHolder;

        private int _detectedID;

        private void OnEnable()
        {
            _detectedID = 0;
            _inputProcessor.ScrolledBack += TrySelectPrevious;
            _inputProcessor.ScrolledForward += TrySelectNext;
            _inputProcessor.Interacted += TryActivateCurrent;
        }
        
        private void OnDisable()
        {
            _inputProcessor.ScrolledBack -= TrySelectPrevious;
            _inputProcessor.ScrolledForward -= TrySelectNext;
            _inputProcessor.Interacted -= TryActivateCurrent;
        }

        private void TrySelectPrevious()
        {
            _interactablesHolder.DetectedInteractables[_detectedID].SetActive(false);
            
            _detectedID++;
            if (_interactablesHolder.DetectedInteractables.Count == _detectedID) _detectedID--;

            _interactablesHolder.DetectedInteractables[_detectedID].SetActive(true);
        }

        private void TrySelectNext()
        {
            _interactablesHolder.DetectedInteractables[_detectedID].SetActive(false);
            
            _detectedID--;
            if (_detectedID < 0) _detectedID++;

            _interactablesHolder.DetectedInteractables[_detectedID].SetActive(true);
        }

        private void TryActivateCurrent()
        {
            var current = _interactablesHolder.DetectedInteractables[_detectedID];
            current.GetComponent<InteractablePresenter>().Activate();
        }
    }
}