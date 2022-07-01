using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class LootInfoReader : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private RectTransform lootInfoPanel;
        [SerializeField] private GameObject displayIconPrefab;

        #endregion

        #region PrivateFields

        private Dictionary<PickableLoot, GameObject> _currentDisplayedIcons = new();

        #endregion

        #region MonoBehaviourMethods

        private void Update()
        {
            foreach (var element in _currentDisplayedIcons.Keys.Where(element => element == null))
            {
                Destroy(_currentDisplayedIcons[element]);
                _currentDisplayedIcons.Remove(element);
                break;
            }
        }

        #endregion
        
        #region TriggerMethods
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PickableLoot>(out var lootComponent) || _currentDisplayedIcons.ContainsKey(lootComponent)) return;

            var displayObject = Instantiate(displayIconPrefab, lootInfoPanel);
            var imageComponents = displayObject.GetComponentsInChildren<Image>();
            var textComponent = displayObject.GetComponentInChildren<Text>();
            var imageComponent = imageComponents.ToList().Find(o => o.gameObject.GetInstanceID() != displayObject.gameObject.GetInstanceID());
            
            if (imageComponent == null | textComponent == null) throw new Exception("Prefab doesnt have image or text component");
            
            imageComponent.sprite = lootComponent.Item.UIImage;
            textComponent.text = "x" + lootComponent.Amount;
            _currentDisplayedIcons.Add(lootComponent, displayObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PickableLoot>(out var lootComponent) || !_currentDisplayedIcons.ContainsKey(lootComponent)) return;

            Destroy(_currentDisplayedIcons[lootComponent]);
            _currentDisplayedIcons.Remove(lootComponent);
        }
        
        #endregion
    }
}
