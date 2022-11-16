using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input;
using _3ClipseGame.Steam.Entities.Player.Data.Specifications.InGame;
using _3ClipseGame.Steam.Entities.Scripts;
using UnityEngine;
using CameraType = _3ClipseGame.Steam.Core.GameSource.Parts.Camera.CameraType;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Player
{
    public abstract class PlayerEntity : Entity
    {
        [SerializeField] protected InputType RelatedInput;
        [SerializeField] protected CameraType RelatedCamera;
        [SerializeField] private PlayerEntityTypes _entityType;
        
        public HealthPoints HealthPoints { get; protected set; }
        public Stamina Stamina { get; protected set; }
        
        public abstract event Action SwitchingToNewEntity;
        public PlayerEntityTypes GetPlayerEntityType() => _entityType;
        
        public abstract void LoseControl();
        public abstract void TakeControl();
    }
}