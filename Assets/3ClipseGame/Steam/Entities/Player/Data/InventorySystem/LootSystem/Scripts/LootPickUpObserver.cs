using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Display;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class LootPickUpObserver : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private ObjectPool pool;

        #endregion
        
        #region PrivateFields

        private InventoryStorage _inventoryStorage;
        private LootInfoReader _lootInfoReader;
        private ChooseOption _lootOptionChooser;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _lootInfoReader = GetComponent<LootInfoReader>();
            _inventoryStorage = GetComponentInParent<InventoryStorage>();
            _lootOptionChooser = GetComponent<ChooseOption>();
        }

        private void OnEnable() => _lootInfoReader.PickUpInitiated += PickUpItem;
        private void OnDisable() => _lootInfoReader.PickUpInitiated -= PickUpItem;

        #endregion

        #region PrivateMethods

        private void PickUpItem(PickableLoot loot)
        {
            _inventoryStorage.AddResources(_lootOptionChooser.CurrentOption.Resource, _lootOptionChooser.CurrentOption.Amount);
            pool.PutObjectInPool(loot.gameObject);
        }

        #endregion
    }
}
