using System;
using _3ClipseGame.Steam.Core.Scripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent
{
    [RequireComponent(typeof(PoolElement))]
    
    public class PooledPickableLoot : PickableLoot
    {
        public override event Action<PickableLoot> Disappeared;
        
        public override void Disappear()
        {
            _poolElementComponent.ReturnToPool();
            Disappeared?.Invoke(this);
        }

        private void Awake()
        {
            _poolElementComponent = GetComponent<PoolElement>();
        }

        private PoolElement _poolElementComponent;
    }
}
