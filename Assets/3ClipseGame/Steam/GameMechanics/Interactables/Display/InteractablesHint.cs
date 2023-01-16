using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesHint : MonoBehaviour
    {
        [SerializeField] private InteractablesDisplay _interactablesDisplay;
        [SerializeField] private RectTransform _interactHint;
        [SerializeField] private RectTransform _scrollHint;

        private void OnEnable()
        {
            _interactablesDisplay.IconCreated += OnIconCreated;
            _interactablesDisplay.IconRetired += OnIconRetired;
        }
        
        private void OnDisable()
        {
            _interactablesDisplay.IconCreated -= OnIconCreated;
            _interactablesDisplay.IconRetired -= OnIconRetired;
        }

        private void OnIconCreated(InteractablePresenter _)
        {
            var iconsAmount = _interactablesDisplay.DisplayedDictionary.Count;
            SwitchInteractHint(true);
            if(iconsAmount == 2) SwitchScrollHint(true);
        }

        private void OnIconRetired()
        {
            var iconsAmount = _interactablesDisplay.DisplayedDictionary.Count;
            if (iconsAmount == 0) SwitchInteractHint(false);
            else if(iconsAmount == 1) SwitchScrollHint(false);
        }

        private void SwitchInteractHint(bool isActive)
        {
            _interactHint.gameObject.SetActive(isActive);
        }

        private void SwitchScrollHint(bool isActive)
        {
            _scrollHint.gameObject.SetActive(isActive);
        }
    }
}
