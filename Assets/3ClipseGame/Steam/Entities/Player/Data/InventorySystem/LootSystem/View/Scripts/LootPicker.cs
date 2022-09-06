using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Detector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootPicker : MonoBehaviour
    {
        [SerializeField] private InputAction _pickUpItem;

        private LootDetector _lootDetector;
        private LootScroller _lootChooser;

        private void Awake()
        {
            _lootDetector = GetComponent<LootDetector>();
            _lootChooser = GetComponent<LootScroller>();
            
            _pickUpItem.Enable();
        }
        
        private void OnEnable()
        {
            _pickUpItem.started += InstantiatePickUp;
        }

        private void OnDisable()
        {
            _pickUpItem.started -= InstantiatePickUp;
        }
        
        #region PickUpHandler

        private void InstantiatePickUp(InputAction.CallbackContext context)
        {
            
        }

        #endregion
    }
}
