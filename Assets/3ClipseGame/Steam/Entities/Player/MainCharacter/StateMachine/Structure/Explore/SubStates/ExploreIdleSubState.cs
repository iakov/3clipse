using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreIdleSubState : MainCharacterExploreSubState
    {
        public ExploreIdleSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        public override void OnStateEnter()
        {
            ExploreDto.SaveLastMove(true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            UpdateInterpolationMove();
        }

        private void UpdateInterpolationMove()
        {
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(ExploreDto.LastMove, Vector3.zero, t * ExploreDto.SpeedInterpolation);
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (IsJumping()) newMainCharacterState = Factory.Jump();
            else if (IsFalling()) newMainCharacterState = Factory.Fall();
            else if (IsCrouching()) newMainCharacterState = Factory.Crouch();
            else if (IsStill()) newMainCharacterState = Factory.Walk();

            return newMainCharacterState != null;
        }

        private bool IsJumping()
        {
            var inputProcessor = ExploreDto.InputProcessor;
            return inputProcessor.GetIsJumpPressed();
        }

        private bool IsFalling()
        {
            var playerController = ExploreDto.PlayerController;
            return !playerController.IsGrounded;
        }

        private bool IsCrouching()
        {
            var inputProcessor = ExploreDto.InputProcessor;
            return inputProcessor.GetIsCrouchPressed();
        }

        private bool IsStill()
        {
            var currentInput = ExploreDto.InputProcessor.GetCurrentInput();
            return currentInput == Vector2.zero;
        }
    }
}