using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreStopSubState : MainCharacterExploreSubState
    {
        public ExploreStopSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory){}

        public override void OnStateEnter()
        {
           ExploreDto.SaveLastMove(true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            UpdateMove();
        }

        private void UpdateMove()
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
            else if (IsStill()) newMainCharacterState = Factory.Idle();
            else if (IsMoving()) newMainCharacterState = Factory.Walk();

            return newMainCharacterState != null;
        }

        private bool IsJumping()
        {
            return ExploreDto.InputProcessor.GetIsJumpPressed();
        }

        private bool IsFalling()
        {
            return !ExploreDto.PlayerController.IsGrounded;
        }

        private bool IsStill()
        {
            var lastMove = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, false);
            return lastMove == Vector3.zero;
        }

        private bool IsMoving()
        {
            var currentInput = ExploreDto.InputProcessor.GetCurrentInput();
            return currentInput != Vector2.zero;
        }
    }
}