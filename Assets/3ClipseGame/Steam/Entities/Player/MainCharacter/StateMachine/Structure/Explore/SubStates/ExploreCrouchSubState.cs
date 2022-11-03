using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreCrouchSubState : MainCharacterExploreSubState
    {
        public ExploreCrouchSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            UpdateMove();
        }

        private void UpdateMove()
        {
            var rawInput = GetCurrentInput();
            var speedModifier = ExploreDto.WalkSpeed * ExploreDto.CrouchSpeedModifier;
            var moveVector = rawInput * speedModifier;
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateWithCamera);
        }

        private Vector3 GetCurrentInput()
        {
            var currentInput = ExploreDto.InputProcessor.GetCurrentInput();
            return new Vector3(currentInput.x, 0f, currentInput.y);
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (IsFalling()) newMainCharacterState = Factory.Fall();
            else if (!IsCrouching()) newMainCharacterState = Factory.Idle();
            
            return newMainCharacterState != null;
        }

        private bool IsFalling()
        {
            var controller = ExploreDto.PlayerController;
            return !controller.IsGrounded;
        }

        private bool IsCrouching()
        { 
            var inputProcessor = ExploreDto.InputProcessor;
            return inputProcessor.GetIsCrouchPressed();
        }
    }
}