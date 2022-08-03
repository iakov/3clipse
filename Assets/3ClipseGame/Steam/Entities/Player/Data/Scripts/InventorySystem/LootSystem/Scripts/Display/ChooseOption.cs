using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Display
{
    public class ChooseOption : MonoBehaviour
    {
        #region PublicFields

        public LootDisplay CurrentOption => _displayedGameObjects.Count > 0 ? _displayedGameObjects[_currentID].GetComponent<LootDisplay>() : null;

        #endregion
        
        #region PrivateFields

        [SerializeField] private InputAction slide;
        [SerializeField] private LootInfoReader lootInfoReader;
        private int _currentID = 0;

        private List<GameObject> _displayedGameObjects;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => slide.Enable();
        private void Start() => _displayedGameObjects = new List<GameObject>();

        private void OnEnable()
        {
            lootInfoReader.DisplayListChanged += OnDisplayListChanged;
            lootInfoReader.PickUpFinished += EditCurrent;
        }

        private void OnDisable()
        {
            lootInfoReader.DisplayListChanged -= OnDisplayListChanged;
            lootInfoReader.PickUpFinished -= EditCurrent;
        }

        #endregion

        #region DisplayListChangedEventHandler

        private void OnDisplayListChanged(GameObject changedObject, bool isAdded)
        {
            if (isAdded) _displayedGameObjects.Add(changedObject);
            else _displayedGameObjects.Remove(changedObject);
            
            ActivateWheel();
            EditCurrentID();
            
            if (_displayedGameObjects.Count == 0) return;
            ActivateHighlight();
        }
        
        private void ActivateWheel()
        {
            if (_displayedGameObjects.Count == 0) slide.started -= Scroll;
            else slide.started += Scroll;
        }

        private void EditCurrentID()
        {
            if (_displayedGameObjects.Count == 0 || _currentID < 0) _currentID = 0;

            if (_currentID > _displayedGameObjects.Count - 1)
                _currentID = _displayedGameObjects.Count - 1;
        }

        private void EditCurrent()
        {
            _displayedGameObjects.RemoveAt(_currentID);
            EditCurrentID();

            if (_displayedGameObjects.Count == 0) return;
            ActivateHighlight();
        }

        private void ActivateHighlight()
        {
            foreach (var icon in _displayedGameObjects) icon.GetComponent<LootDisplay>().SetActive(false);
            _displayedGameObjects[_currentID].GetComponent<LootDisplay>().SetActive(true);
        }

        #endregion

        #region PrivateMethods

        private void Scroll(InputAction.CallbackContext context)
        {
            var isUp = context.ReadValue<float>() > 0;

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

        #endregion
    }
}
