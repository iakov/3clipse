using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.Dropper;
using UnityEngine;
using Resource = _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts.Resource;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent
{
    public abstract class PickableLoot : MonoBehaviour
    {
        #region Abstract

        public abstract event Action<PickableLoot> Disappeared;
        
        public abstract void Disappear();

        #endregion
        
        public event Action TrackedElementUpdated;
        [SerializeField] private DropElement _trackedDropElement;
        
        public Resource GetResource()
        {
            return _trackedDropElement.GetResource();
        }

        public int GetAmount()
        {
            return _trackedDropElement?.GetFinalAmountOfDrop() ?? 0;
        }

        public void SetDropElement(DropElement element)
        {
            _trackedDropElement = element;
            TrackedElementUpdated?.Invoke();
        }
    }
}