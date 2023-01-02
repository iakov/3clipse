using System;
using _3ClipseGame.Steam.GameCore.Origin;
using _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic
{
    public class LootInteractable : Interactable
    {
        [SerializeField] private GameObject _lootPresenter;
        
        public override event Action<Interactable> Disappeared;
        
        private Resource _resource;
        private int _amount;

        public void ChangeData(Resource resource, int amount)
        {
            _resource = resource;
            _amount = amount;
        }
        
        public override InteractablePresenter GetNewPresenter()
        { 
            var newPresenter = Instantiate(_lootPresenter).GetComponent<LootPresenter>();
            newPresenter.ChangeInteractable(this);
            newPresenter.SetResource(_resource);
            newPresenter.SetAmount(_amount);
            return newPresenter;
        }

        public override void Activate()
        {
            var player = GameSource.Instance.GetPlayer();
            var currentEntityInventory = player.GetCurrentPlayerEntity().Inventory;
            currentEntityInventory.TryAddItem(_resource, _amount);
        }
    }
}