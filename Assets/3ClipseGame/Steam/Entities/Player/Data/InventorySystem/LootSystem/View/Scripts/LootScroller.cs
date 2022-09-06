using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootScroller : MonoBehaviour
    {
        public PickableLoot CurrentSelectedLoot { get; private set; }

        [Header("Selection")]
        [SerializeField] private InputAction _slideAction;
        [SerializeField] private LootDisplay _lootDisplay;
        [Header("Slide view")]
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private VerticalLayoutGroup _verticalLayout;
        [SerializeField] private ScrollRect _scrollRect;
        
        private int _currentID;
        private float _lastScrollValue;
        
        private  float _viewportHeight => _scrollbar.GetComponent<RectTransform>().rect.height;
        private float _oneIconHeight => CurrentSelectedLoot.GetComponent<RectTransform>().rect.height;
        private  float _allIconsHeight => _oneIconHeight * _lootDisplay.DisplayedLootAndItsIcons.Count;
        private float _allSpacesHeight => _verticalLayout.spacing * (_lootDisplay.DisplayedLootAndItsIcons.Count - 1);
        private float _fullContentHeight => _allIconsHeight + _allSpacesHeight;
        private float _upperBound => (_fullContentHeight - _viewportHeight) * (1-_scrollbar.value);

        private void Awake()
        {
            _slideAction.Enable();
        }

        #region Enable

        private void OnEnable()
        {
            EnableLootDisplayEvents();
            EnableScrollEvents();

        }
        
        private void EnableLootDisplayEvents()
        {
            _lootDisplay.LootDisplayListIncreased += OnLootDisplayListIncreased;
            _lootDisplay.LootDisplayListDecreased += OnLootDisplayListDecreased;
        }

        private void EnableScrollEvents()
        {
            _slideAction.started += OnScroll;
            _slideAction.performed += OnScroll;
            _slideAction.canceled += OnScroll;
        }

        #endregion

        #region Disable

        private void OnDisable()
        {
            DisableLootDisplayEvents();
            DisableScrollEvents();
        }
        
        private void DisableLootDisplayEvents()
        {
            _lootDisplay.LootDisplayListIncreased -= OnLootDisplayListIncreased;
            _lootDisplay.LootDisplayListDecreased -= OnLootDisplayListDecreased;
        }
        
        private void DisableScrollEvents()
        {
            _slideAction.started -= OnScroll;
            _slideAction.performed -= OnScroll;
            _slideAction.canceled -= OnScroll;
        }

        #endregion
        
        private void OnLootDisplayListIncreased(){}
        
        private void OnLootDisplayListDecreased(){}
        
        

        private void OnScroll(InputAction.CallbackContext context)
        {
            SetNewSelectedSlot();
            SetScroll();
        }

        private void SetNewSelectedSlot()
        {
            
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
    }
}
