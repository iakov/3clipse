using System.Collections;
using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Scripts.Display;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Display
{
    public class ChooseOption : MonoBehaviour
    {
        #region PublicFields

        public LootDisplay CurrentOption => _displayedGameObjects.Count > 0 ? _displayedGameObjects[_currentID].GetComponent<LootDisplay>() : null;

        #endregion
        
        #region PrivateFields

        [Header("Selection")]
        [SerializeField] private InputAction slide;
        [SerializeField] private LootInfoReader lootInfoReader;
        [Header("Slide view")]
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private VerticalLayoutGroup verticalLayout;
        [SerializeField] private ScrollRect scrollRect;
        
        private int _currentID = 0;
        private float _lastScrollValue;
        
        private  float _viewportHeight => scrollbar.GetComponent<RectTransform>().rect.height;
        private float _oneIconHeight => _displayedGameObjects[_currentID].GetComponent<RectTransform>().rect.height;
        private  float _allIconsHeight => _oneIconHeight * _displayedGameObjects.Count;
        private float _allSpacesHeight => verticalLayout.spacing * (_displayedGameObjects.Count - 1);
        private float _fullContentHeight => _allIconsHeight + _allSpacesHeight;
        private float _upperBound => (_fullContentHeight - _viewportHeight) * (1-scrollbar.value);
        

        private List<GameObject> _displayedGameObjects;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => slide.Enable();
        private void Start() => _displayedGameObjects = new List<GameObject>();

        private void OnEnable()
        {
            lootInfoReader.DisplayListChanged += OnDisplayListChanged;
            lootInfoReader.PickUpFinished += OnPickUp;
        }

        private void OnDisable()
        {
            lootInfoReader.DisplayListChanged -= OnDisplayListChanged;
            lootInfoReader.PickUpFinished -= OnPickUp;
        }

        #endregion

        #region DisplayListChangedHandler

        private void OnDisplayListChanged(GameObject changedObject, bool isAdded)
        {
            if (isAdded) _displayedGameObjects.Add(changedObject);
            else _displayedGameObjects.Remove(changedObject);
            
            ActivateWheel();
            EditCurrentID();

            if (_displayedGameObjects.Count == 0) return;
            ActivateHighlight();
            if (isAdded) StartCoroutine(ScrollTo(0.2f, 1f));
        }

        private IEnumerator ScrollTo(float time, float finalScroll)
        {
            yield return null;

            var t = 0f;
            var timeModifier = 1 / time;
            var currentScroll = scrollbar.value;
            
            while (t < time)
            {
                scrollRect.verticalNormalizedPosition = Mathf.Lerp(currentScroll, finalScroll, t * timeModifier);
                t += Time.deltaTime;
                yield return null;
            }

            scrollRect.verticalNormalizedPosition = finalScroll;
        }

        private void ActivateWheel()
        {
            if (_displayedGameObjects.Count == 0) slide.started -= OnScroll;
            else slide.started += OnScroll;
        }

        private void EditCurrentID()
        {
            if (_displayedGameObjects.Count == 0 || _currentID < 0) _currentID = 0;

            if (_currentID > _displayedGameObjects.Count - 1) _currentID = _displayedGameObjects.Count - 1;
        }

        private void ActivateHighlight()
        {
            foreach (var icon in _displayedGameObjects) icon.GetComponent<LootDisplay>().SetActive(false);
            _displayedGameObjects[_currentID].GetComponent<LootDisplay>().SetActive(true);
        }

        #endregion

        #region ScrollHandler

        private void OnScroll(InputAction.CallbackContext context)
        {
            var isUp = context.ReadValue<float>() > 0;
            UpdateHighlight(isUp);
            SetScroll();
        }

        private void UpdateHighlight(bool isUp)
        {
            switch (isUp)
            {
                case true when _currentID > 0:
                    _currentID--;
                    ActivateHighlight();
                    break;
                case false when _currentID < _displayedGameObjects.Count - 1:
                    _currentID++;
                    ActivateHighlight();
                    break;
            }
        }

        private void SetScroll()
        {
            var scrollHeight = _oneIconHeight / _fullContentHeight / (1 - scrollbar.size);

            var currentTargetPosition = (_currentID + 0.5f) * _oneIconHeight + _currentID * verticalLayout.spacing;
            var upper = _upperBound;
            var delta = currentTargetPosition - upper;

            if (delta > _viewportHeight && scrollbar.value > 0) scrollRect.verticalNormalizedPosition -= scrollHeight;
            if (delta < _oneIconHeight / 2 && scrollbar.value < 1) scrollRect.verticalNormalizedPosition += scrollHeight;

            if (scrollbar.value < 0) scrollbar.value = 0;
            if (scrollbar.value > 1) scrollbar.value = 1;
        }

        #endregion

        #region PickUpHandler

        private void OnPickUp()
        {
            _displayedGameObjects.RemoveAt(_currentID);
            EditCurrentID();

            if (_displayedGameObjects.Count == 0) return;
            ActivateHighlight();
        }

        #endregion
    }
}
