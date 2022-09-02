using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts;
using _3ClipseGame.Steam.Global.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker
{
    public class LootPickUpObserver : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private ObjectPool _pool;

        #endregion
        
        #region PrivateFields

        private InventoryStorage _inventoryStorage;
        private LootDetector _lootDetector;
        private OptionsScroller _lootOptionChooser;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _lootDetector = GetComponent<LootDetector>();
            _inventoryStorage = GetComponentInParent<InventoryStorage>();
            _lootOptionChooser = GetComponent<OptionsScroller>();
        }

        private void OnEnable()
        {
            _lootDetector.PickUpInitiated += PickUpItem;
        }

        private void OnDisable()
        {
            _lootDetector.PickUpInitiated -= PickUpItem;
        }

        #endregion

        #region PrivateMethods

        private void PickUpItem(PickableLoot loot)
        {
            _inventoryStorage.AddResources(_lootOptionChooser.CurrentOption.Resource, _lootOptionChooser.CurrentOption.Amount);
            _pool.PutObjectInPool(loot.gameObject);
        }

        #endregion
    }
}
