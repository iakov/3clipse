using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates
{
    public class ExploreStopSubState : MainCharacterExploreSubState
    {
        public ExploreStopSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory){}

        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, true);
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

        public override bool TrySwitchState(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (ExploreDto.InputProcessor.GetIsJumpPressed()) newMainCharacterState = Factory.Jump();
            else if (!ExploreDto.PlayerController.IsGrounded && !Physics.Raycast(ExploreDto.Transform.position, Vector3.down,
                    ExploreDto.PlayerController.Radius)) newMainCharacterState = Factory.Fall();
            else if (ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, false) == Vector3.zero) newMainCharacterState = Factory.Idle();
            else if (ExploreDto.InputProcessor.GetCurrentInput() != Vector2.zero) newMainCharacterState = Factory.Walk();

            return newMainCharacterState != null;
        }
    }
}