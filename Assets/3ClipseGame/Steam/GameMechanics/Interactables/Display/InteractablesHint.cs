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
            _interactablesDisplay.IconRetiring += OnIconRetiring;
        }
        
        private void OnDisable()
        {
            _interactablesDisplay.IconCreated -= OnIconCreated;
            _interactablesDisplay.IconRetiring -= OnIconRetiring;
        }

        private void OnIconCreated(InteractablePresenter newIcon)
        {
            var iconsAmount = _interactablesDisplay.DisplayedDictionary.Count;
            SwitchInteractHint(true);
            if(iconsAmount == 2) SwitchScrollHint(true);
        }

        private void OnIconRetiring(InteractablePresenter retiringIcon)
        {
            var iconsAmount = _interactablesDisplay.DisplayedDictionary.Count;
            if (iconsAmount == 1) SwitchInteractHint(false);
            else if(iconsAmount == 2) SwitchScrollHint(false);
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
