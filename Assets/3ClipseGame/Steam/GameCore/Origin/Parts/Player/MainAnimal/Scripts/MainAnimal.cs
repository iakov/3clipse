using System;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Interfaces;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Parts.Specifications.InGame;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterController;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.Scripts
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
            Stamina = GetComponent<Stamina>();
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
