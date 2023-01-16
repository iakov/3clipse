using _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic
{
    public class LootPresenter : InteractablePresenter
    {
        [SerializeField] private TMP_Text _resourceNameComponent;
        [SerializeField] private TMP_Text _resourceAmountComponent;

        public override void Activate() => CurrentInteractable.Activate();

        public void SetResource(Resource resource)
        {
            _resourceNameComponent.text = resource.Name;
        }

        public void SetAmount(int amount)
        {
            _resourceAmountComponent.text = amount.ToString();
        }
    }
}