using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates
{
    public class ExploreJumpSubState : MainCharacterExploreSubState
    {
        public ExploreJumpSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;
            var jumpMoveVector = _lastMoveVector + Vector3.up * ExploreDto.JumpStrength;
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, jumpMoveVector, RotationType.NoRotation);
            
            ExploreDto.Stamina.IsRecovering = false;
            ExploreDto.Stamina.AddValue(ExploreDto.JumpStaminaReduce);
        }

        public override void OnStateExit()
        {
            ExploreDto.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (ExploreDto.PlayerController.IsGrounded && StateTimer > 0.1f) newMainCharacterState = Factory.Idle();
            else if(ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, false).y + ExploreDto.PlayerMover.GetLastMove(MoveType.GravityMove, false).y < 0) newMainCharacterState = Factory.Fall();

            return newMainCharacterState != null;
        }
    }
}
