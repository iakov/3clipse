using System;
using _3ClipseGame.Steam.GameMechanics.LootSystem.InGame.Scripts.Dropper;
using Mono.Cecil;
using UnityEngine;
using Resource = _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts.Resource;

namespace _3ClipseGame.Steam.GameMechanics.LootSystem.InGame.Scripts.LootComponent
{
    public abstract class PickableLoot : MonoBehaviour
    {
        #region Abstract

        public abstract event Action<PickableLoot> Disappeared;
        public abstract void Disappear();

        #endregion
        
        [SerializeField] private DropElement _trackedDropElement;
        
        public event Action TrackedElementUpdated;
        
        public Resource GetResource()
        {
            return _trackedDropElement?.GetResource();
        }

        public int GetAmount()
        {
            return _trackedDropElement?.GetFinalAmountOfDrop() ?? 0;
        }

        public void SetDropElement(DropElement element)
        {
            if(element == null) return;
            
            _trackedDropElement = element;
            TrackedElementUpdated?.Invoke();
        }
    }
}