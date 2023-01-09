using _3ClipseGame.Steam.GameMechanics.Interactables.Detector;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesDisplay : MonoBehaviour
    {
        [SerializeField] private DetectedInteractablesHolder _interactablesHolder;
        [SerializeField] private Transform _iconsParent;

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
            var originalPresenter = interactable.GetNewPresenter();
            var presenter = Instantiate(originalPresenter, Vector3.zero, Quaternion.identity, _iconsParent);
            presenter.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            presenter.ChangeInteractable(interactable);
            if(DisplayedDictionary.Count != 0) presenter.gameObject.SetActive(false);
            DisplayedDictionary.AddElement(interactable, presenter);
        }
        
        private void RemoveInteractable(Interactable interactable)
        {
            if (DisplayedDictionary.Contains(interactable) == false) return;
            var presenter = DisplayedDictionary.GetValueByKey(interactable);

            DisplayedDictionary.RemoveElement(interactable);
            Destroy(((InteractablePresenter) presenter).gameObject);
        }
    }
}