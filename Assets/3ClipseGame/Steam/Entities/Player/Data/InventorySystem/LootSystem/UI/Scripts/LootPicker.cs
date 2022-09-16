using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Detector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.UI.Scripts
{
    public class LootPicker : MonoBehaviour
    {
        #region Serialization

        [SerializeField] private InputAction _pickUpItem;

        #endregion

        #region Initialization

        private LootDetector _lootDetector;
        private LootIconsSelector _lootChooser;

        private void Awake()
        {
            _lootDetector = GetComponent<LootDetector>();
            _lootChooser = GetComponent<LootIconsSelector>();
            
            _pickUpItem.Enable();
        }

        #endregion

        private void OnEnable()
        {
            _pickUpItem.started += InstantiatePickUp;
        }

        private void OnDisable()
        {
            _pickUpItem.started -= InstantiatePickUp;
        }
        
        private void InstantiatePickUp(InputAction.CallbackContext context)
        {
            
        }
    }
}
