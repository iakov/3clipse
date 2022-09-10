using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts.LootIconsListControls
{
    public class LootScroller : MonoBehaviour
    {
        [Header("Slide view")]
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private RectTransform _lootIcon;
        
        private VerticalLayoutGroup _verticalLayout;
        private ScrollRect _scrollRect;

        private LootDisplay _lootDisplay;
        private LootIconsSelector _lootIconsSelector;

        private void Awake()
        {
            _lootDisplay = GetComponent<LootDisplay>();
            _lootIconsSelector = GetComponent<LootIconsSelector>();
            _verticalLayout = GetComponent<VerticalLayoutGroup>();
            _scrollRect = GetComponentInParent<ScrollRect>();
        }

        private void OnEnable()
        {
            _lootIconsSelector.SelectedLootChanged += OnSelectedLootChanged;
        }

        private void OnDisable()
        {
            _lootIconsSelector.SelectedLootChanged -= OnSelectedLootChanged;
        }

        private void OnSelectedLootChanged(PickableLoot previousLoot)
        {
            //Edit scroll value
        }
    }
}