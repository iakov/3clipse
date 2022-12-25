using System;
using _3ClipseGame.Steam.GameCore.GlobalScripts.Pool;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.LootComponent
{
    [RequireComponent(typeof(PoolElement))]
    
    public class PooledPickableLoot : ResourcePickableLoot
    {
        public override event Action<Interactable> Disappeared;
        
        private PoolElement _poolElementComponent;
        
        private void Awake() 
            => _poolElementComponent = GetComponent<PoolElement>();
        
        public override void Disappear()
        {
            _poolElementComponent.ReturnToPool();
            Disappeared?.Invoke(this);
        }
    }
}
