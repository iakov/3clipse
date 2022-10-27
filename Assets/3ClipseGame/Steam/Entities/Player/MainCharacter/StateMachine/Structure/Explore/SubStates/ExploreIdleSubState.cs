using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreIdleSubState : MainCharacterExploreSubState
    {
        public ExploreIdleSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * ExploreDto.SpeedInterpolation);
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (ExploreDto.InputProcessor.GetIsJumpPressed()) newMainCharacterState = Factory.Jump();
            else if (!ExploreDto.PlayerController.IsGrounded) newMainCharacterState = Factory.Fall();
            else if (ExploreDto.InputProcessor.GetIsCrouchPressed()) newMainCharacterState = Factory.Crouch();
            else if (ExploreDto.InputProcessor.GetCurrentInput() != Vector2.zero) newMainCharacterState = Factory.Walk();

            return newMainCharacterState != null;
        }
    }
}