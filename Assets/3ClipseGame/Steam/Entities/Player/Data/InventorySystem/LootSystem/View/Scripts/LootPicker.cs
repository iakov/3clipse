using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Detector;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts.LootIconsListControls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootPicker : MonoBehaviour
    {
        [SerializeField] private InputAction _pickUpItem;

        private LootDetector _lootDetector;
        private LootIconsSelector _lootChooser;

        private void Awake()
        {
            _lootDetector = GetComponent<LootDetector>();
            _lootChooser = GetComponent<LootIconsSelector>();
            
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
        
        private void InstantiatePickUp(InputAction.CallbackContext context)
        {
            
        }
    }
}
