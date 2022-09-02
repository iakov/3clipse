using System;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    [RequireComponent(typeof(SphereCollider))]
    public class LootDetector : MonoBehaviour
    {
        #region Events

        public Action<GameObject, bool> DisplayListChanged;
        
        public Action<PickableLoot> PickUpInitiated;
        public Action PickUpFinished;

        #endregion
        
        #region SerializeFields

        [SerializeField] private RectTransform _lootInfoPanel;
        [SerializeField] private GameObject _displayIconPrefab;
        [SerializeField] private InputAction _pickUpItem;

        #endregion

        #region PrivateFields
        
        private OptionsScroller _optionChooser;
        private Dictionary<PickableLoot, GameObject> _currentDisplayedIcons = new();

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _pickUpItem.Enable();
            _optionChooser = GetComponent<OptionsScroller>();
        }

        private void Update()
        {
            foreach (var element in _currentDisplayedIcons.Keys.Where(element => !element))
            {
                Destroy(_currentDisplayedIcons[element]);
                _currentDisplayedIcons.Remove(element);
            }
        }

        private void OnEnable()
        {
            _pickUpItem.started += InstantiatePickUp;
        }

        private void OnDisable()
        {
            _pickUpItem.started -= InstantiatePickUp;
        }

        #endregion
        
        #region PickUpHandler

        private void InstantiatePickUp(InputAction.CallbackContext context)
        {
            if (_currentDisplayedIcons.Count == 0) return;
            
            var pickedLoot = _currentDisplayedIcons.First(element => _optionChooser.CurrentOption.gameObject == element.Value.gameObject);
            PickUpInitiated?.Invoke(pickedLoot.Key);
            
            _currentDisplayedIcons.Remove(pickedLoot.Key);
            Destroy(pickedLoot.Value);
            
            PickUpFinished?.Invoke();
        }

        #endregion
        
        #region TriggerMethods
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PickableLoot>(out var lootComponent) || _currentDisplayedIcons.ContainsKey(lootComponent)) return;

            var displayObject = Instantiate(_displayIconPrefab, _lootInfoPanel);

            var lootDisplay = displayObject.GetComponent<LootIcon>();
            if (lootDisplay == null) throw new Exception("Prefab doesnt have image or text component");
            
            lootDisplay.Resource = lootComponent.Resource;
            lootDisplay.Amount =  lootComponent.Amount;
            _currentDisplayedIcons.Add(lootComponent, displayObject);
            
            DisplayListChanged?.Invoke(displayObject, true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PickableLoot>(out var lootComponent) || !_currentDisplayedIcons.ContainsKey(lootComponent)) return;

            var destroyed = _currentDisplayedIcons[lootComponent];
            DisplayListChanged?.Invoke(destroyed, false);
            
            Destroy(_currentDisplayedIcons[lootComponent]);
            _currentDisplayedIcons.Remove(lootComponent);
        }
        
        #endregion
    }
}
