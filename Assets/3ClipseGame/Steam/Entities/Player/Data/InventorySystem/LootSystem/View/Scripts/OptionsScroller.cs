using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class OptionsScroller : MonoBehaviour
    {
        #region PublicFields

        public LootIcon CurrentOption => _displayedGameObjects.Count > 0 ? _displayedGameObjects[_currentID].GetComponent<LootIcon>() : null;

        #endregion
        
        #region PrivateFields

        [Header("Selection")]
        [SerializeField] private InputAction _slideAction;
        [SerializeField] private LootDetector _lootDetector;
        [Header("Slide view")]
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private VerticalLayoutGroup _verticalLayout;
        [SerializeField] private ScrollRect _scrollRect;
        
        private int _currentID;
        private float _lastScrollValue;
        
        private  float _viewportHeight => _scrollbar.GetComponent<RectTransform>().rect.height;
        private float _oneIconHeight => _displayedGameObjects[_currentID].GetComponent<RectTransform>().rect.height;
        private  float _allIconsHeight => _oneIconHeight * _displayedGameObjects.Count;
        private float _allSpacesHeight => _verticalLayout.spacing * (_displayedGameObjects.Count - 1);
        private float _fullContentHeight => _allIconsHeight + _allSpacesHeight;
        private float _upperBound => (_fullContentHeight - _viewportHeight) * (1-_scrollbar.value);
        

        private List<GameObject> _displayedGameObjects;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _slideAction.Enable();
        }

        private void Start()
        {
            _displayedGameObjects = new List<GameObject>();
        }

        private void OnEnable()
        {
            _lootDetector.DisplayListChanged += OnDisplayListChanged;
            _lootDetector.PickUpFinished += OnPickUp;
        }

        private void OnDisable()
        {
            _lootDetector.DisplayListChanged -= OnDisplayListChanged;
            _lootDetector.PickUpFinished -= OnPickUp;
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
            var currentScroll = _scrollbar.value;
            
            while (t < time)
            {
                _scrollRect.verticalNormalizedPosition = Mathf.Lerp(currentScroll, finalScroll, t * timeModifier);
                t += Time.deltaTime;
                yield return null;
            }

            _scrollRect.verticalNormalizedPosition = finalScroll;
        }

        private void ActivateWheel()
        {
            if (_displayedGameObjects.Count == 0) _slideAction.started -= OnScroll;
            else _slideAction.started += OnScroll;
        }

        private void EditCurrentID()
        {
            if (_displayedGameObjects.Count == 0 || _currentID < 0) _currentID = 0;

            else if (_currentID > _displayedGameObjects.Count - 1) _currentID = _displayedGameObjects.Count - 1;
        }

        private void ActivateHighlight()
        {
            foreach (var icon in _displayedGameObjects) icon.GetComponent<LootIcon>().SetActive(false);
            _displayedGameObjects[_currentID].GetComponent<LootIcon>().SetActive(true);
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
            var scrollHeight = _oneIconHeight / _fullContentHeight / (1 - _scrollbar.size);

            var currentTargetPosition = (_currentID + 0.5f) * _oneIconHeight + _currentID * _verticalLayout.spacing;
            var upper = _upperBound;
            var delta = currentTargetPosition - upper;

            if (delta > _viewportHeight && _scrollbar.value > 0) _scrollRect.verticalNormalizedPosition -= scrollHeight;
            if (delta < _oneIconHeight / 2 && _scrollbar.value < 1) _scrollRect.verticalNormalizedPosition += scrollHeight;

            if (_scrollbar.value < 0) _scrollbar.value = 0;
            if (_scrollbar.value > 1) _scrollbar.value = 1;
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
