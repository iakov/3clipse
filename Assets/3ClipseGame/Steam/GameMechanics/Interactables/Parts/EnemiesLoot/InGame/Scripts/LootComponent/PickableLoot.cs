using System;
using _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.Dropper;
using UnityEngine;
using Resource = _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts.Resource;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.LootComponent
{
    public abstract class PickableLoot : Interactable
    {
        [SerializeField] private DropElement _trackedDropElement;
        
        public event Action TrackedElementUpdated;
        public abstract void Disappear();
        
        public Resource GetResource() 
            => _trackedDropElement?.GetResource();

        public int GetAmount() 
            => _trackedDropElement?.GetFinalAmountOfDrop() ?? 0;

        public void SetDropElement(DropElement element)
        {
            if(element == null) return;
            
            _trackedDropElement = element;
            TrackedElementUpdated?.Invoke();
        }
    }
}