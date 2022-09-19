using System;
using _3ClipseGame.Steam.Global.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent
{
    [RequireComponent(typeof(PoolElement))]
    
    public class PooledPickableLoot : PickableLoot
    {
        #region Public

        public override event Action<PickableLoot> Disappeared;
        
        public override void Disappear()
        {
            _poolElementComponent.ReturnToPool();
            Disappeared?.Invoke(this);
        }

        #endregion

        #region Initialization

        private void Awake()
        {
            _poolElementComponent = GetComponent<PoolElement>();
        }

        private PoolElement _poolElementComponent;

        #endregion
    }
}
