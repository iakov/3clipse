using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Display;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class LootPickUpObserver : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private ChooseOption lootOptionChooser;

        #endregion

        #region PrivateMethods

        private InventoryStorage _inventoryStorage;
        private LootInfoReader _lootInfoReader;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _lootInfoReader = GetComponentInChildren<LootInfoReader>();
            _inventoryStorage = GetComponent<InventoryStorage>();
        }

        private void OnEnable() => _lootInfoReader.PickUpInitiated += PickUpItem;
        private void OnDisable() => _lootInfoReader.PickUpInitiated -= PickUpItem;

        #endregion

        #region PrivateMethods

        private void PickUpItem() => 
            _inventoryStorage.AddResources(lootOptionChooser.CurrentOption.Resource, lootOptionChooser.CurrentOption.Amount);

        #endregion
    }
}
