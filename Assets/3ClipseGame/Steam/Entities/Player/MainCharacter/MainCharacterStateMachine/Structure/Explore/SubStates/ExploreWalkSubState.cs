using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates
{
    public class ExploreWalkSubState : MainCharacterExploreSubState
    {
        public ExploreWalkSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory){}

        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void OnStateEnter()
        {
            ExploreDto.CharacterAnimator.SetBool(IsWalking, true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            Move();
            Rotate();
        }

        public override void OnStateExit()
        {
            ExploreDto.CharacterAnimator.SetBool(IsWalking, false);
        }

        public override bool TrySwitchState(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (!ExploreDto.PlayerController.IsGrounded && !ExploreDto.PlayerController.IsGrounded) newMainCharacterState = Factory.Fall();
            else if (ExploreDto.InputProcessor.GetIsJumpPressed()) newMainCharacterState = Factory.Jump();
            else if (ExploreDto.InputProcessor.GetCurrentInput() == Vector2.zero) newMainCharacterState = Factory.Stop();
            else if (ExploreDto.InputProcessor.GetIsSprintPressed() && ExploreDto.Stamina.StaminaPercentage > ExploreDto.MinRunEntryStamina) newMainCharacterState = Factory.Run();
            else if (ExploreDto.InputProcessor.GetIsCrouchPressed()) newMainCharacterState = Factory.Crouch();
            
            return newMainCharacterState != null;
        }
        
        private void Move()
        {
            var rawMoveVector = new Vector3(ExploreDto.InputProcessor.GetCurrentInput().x, 0f, ExploreDto.InputProcessor.GetCurrentInput().y);
            var moveVector = rawMoveVector * ExploreDto.WalkSpeed;
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        private void Rotate()
        {
            var rotatedMove = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            
            ExploreDto.PlayerController.Rotate(Quaternion.LookRotation(rotatedMove));
        }
    }
}