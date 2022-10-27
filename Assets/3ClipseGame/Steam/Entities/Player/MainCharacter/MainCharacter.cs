using System;
using _3ClipseGame.Steam.Core.GameSource;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter
{
    [RequireComponent(typeof(StateMachine.MainCharacterStateMachine))]
    [RequireComponent(typeof(PlayerMover))]
    public class MainCharacter : PlayerEntity
    {
        public override event Action SwitchingToNewEntity;
        
        private StateMachine.MainCharacterStateMachine _mainCharacterStateMachine;
        [SerializeField] private MovementInputHandler _movementInputHandler;
        
        public override void LoseControl()
        {
            GameSource.Instance.GetInputManager().Disable(RelatedInput);
        }

        public override void TakeControl()
        {
            GameSource.Instance.GetInputManager().Enable(RelatedInput);
            GameSource.Instance.GetCameraManager().Enable(RelatedCamera);
        }

        private void Awake()
        {
            _mainCharacterStateMachine = GetComponent<StateMachine.MainCharacterStateMachine>();
        }
        
        private void Update()
        {
            _mainCharacterStateMachine.UpdateWork();
        }

        private void OnEnable() 
            => _movementInputHandler.SwitchToAnotherEntityPressed += OnSwitchPressed;

        private void OnDisable()
            => _movementInputHandler.SwitchToAnotherEntityPressed -= OnSwitchPressed;
        
        private void OnSwitchPressed()
            => SwitchingToNewEntity?.Invoke();
    }
}
