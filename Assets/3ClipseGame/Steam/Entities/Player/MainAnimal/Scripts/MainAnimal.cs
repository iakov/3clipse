using System;
using _3ClipseGame.Steam.Core.GameSource;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class MainAnimal : PlayerEntity
    {
        [SerializeField] private MovementInputHandler _movementInputHandler;
        
        public override event Action SwitchingToNewEntity;
        
        public override void LoseControl()
        {
            GameSource.Instance.GetInputManager().Disable(RelatedInput);
        }

        public override void TakeControl()
        {
            GameSource.Instance.GetInputManager().Enable(RelatedInput);
            GameSource.Instance.GetCameraManager().Enable(RelatedCamera);
        }
        
        private StateMachine.MainAnimalStateMachine _mainAnimalStateMachine;

        private void Awake()
        {
            _mainAnimalStateMachine = GetComponent<StateMachine.MainAnimalStateMachine>();
        }

        private void Update()
        {
            _mainAnimalStateMachine.UpdateWork();
        }
        
        private void OnEnable() 
            => _movementInputHandler.SwitchToAnotherEntityPressed += OnSwitchPressed;

        private void OnDisable()
            => _movementInputHandler.SwitchToAnotherEntityPressed -= OnSwitchPressed;
        
        private void OnSwitchPressed()
            => SwitchingToNewEntity?.Invoke();
    }
}
