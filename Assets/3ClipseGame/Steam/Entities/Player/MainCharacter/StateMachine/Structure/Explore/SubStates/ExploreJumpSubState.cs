using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreJumpSubState : MainCharacterExploreSubState
    {
        public ExploreJumpSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        public override void OnStateEnter()
        {
            ExploreDto.SaveLastMove(true);
            SetJumpMove();
            
            ExploreDto.SwitchStaminaRecovery(false);
            WasteJumpStamina();
        }

        private void SetJumpMove()
        {
            var jumpMoveVector = ExploreDto.LastMove + Vector3.up * ExploreDto.JumpStrength;
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, jumpMoveVector, RotationType.NoRotation);
        }

        private void WasteJumpStamina()
        {
            ExploreDto.Stamina.AddValue(ExploreDto.JumpStaminaReduce);
        }

        public override void OnStateExit()
        {
            ExploreDto.SwitchStaminaRecovery(true);
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (IsStill()) newMainCharacterState = Factory.Idle();
            else if(IsFalling()) newMainCharacterState = Factory.Fall();

            return newMainCharacterState != null;
        }

        private bool IsStill()
        {
            var isGrounded = ExploreDto.PlayerController.IsGrounded;
            var isAbleToSwitch = StateTimer > 0.1f;
            return isGrounded && isAbleToSwitch;
        }

        private bool IsFalling()
        {
            var jumpMoveHeight = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, false).y;
            var gravityMoveHeight = ExploreDto.PlayerMover.GetLastMove(MoveType.GravityMove, false).y;
            var finalMoveHeight = jumpMoveHeight + gravityMoveHeight;

            return finalMoveHeight < 0f;
        }
    }
}
