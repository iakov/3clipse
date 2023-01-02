using _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic
{
    public class LootPresenter : InteractablePresenter
    {
        [SerializeField] private Image _imageComponent;
        [SerializeField] private TMP_Text _resourceNameComponent;
        [SerializeField] private TMP_Text _resourceAmountComponent;
        
        public void SetResource(Resource resource)
        {
            _imageComponent.sprite = resource.UIImage;
            _resourceNameComponent.text = resource.Name;
        }

        public void SetAmount(int amount)
        {
            _resourceAmountComponent.text = amount.ToString();
        }
    }
}