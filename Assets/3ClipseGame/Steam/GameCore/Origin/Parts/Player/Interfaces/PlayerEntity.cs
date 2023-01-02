using System;
using _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Parts.Specifications.InGame;
using _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using UnityEngine;
using CameraType = _3ClipseGame.Steam.GameCore.Origin.Parts.Camera.CameraType;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Interfaces
{
    public abstract class PlayerEntity : Entity
    {
        [SerializeField] protected InputType RelatedInput;
        [SerializeField] protected CameraType RelatedCamera;
        [SerializeField] private PlayerEntityTypes _entityType;
        [SerializeField] private ResourceInventory _inventory;

        public ResourceInventory Inventory => _inventory;
        public HealthPoints HealthPoints { get; protected set; }
        public Stamina Stamina { get; protected set; }
        
        public abstract event Action SwitchingToNewEntity;
        public PlayerEntityTypes GetPlayerEntityType() => _entityType;
        
        public abstract void LoseControl();
        public abstract void TakeControl();
    }
}