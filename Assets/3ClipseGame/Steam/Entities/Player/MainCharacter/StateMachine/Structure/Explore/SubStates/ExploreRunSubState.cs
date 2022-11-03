using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreRunSubState : MainCharacterExploreSubState
    {
        public ExploreRunSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void OnStateEnter()
        {
            UpdateAnimator();
            ExploreDto.SwitchStaminaRecovery(false);
        }

        private void UpdateAnimator()
        {
            ExploreDto.CharacterAnimator.SetBool(IsRunning, true);
            ExploreDto.CharacterAnimator.SetBool(IsWalking, true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            ChangeMove();
            ChangeRotation();
            ReduceStamina();
        }
        
        private void ChangeMove()
        {
            var rawMoveVector = new Vector3(ExploreDto.InputProcessor.GetCurrentInput().x, 0f, ExploreDto.InputProcessor.GetCurrentInput().y);
            var moveVector = rawMoveVector * (ExploreDto.RunModifierCurve.Evaluate(StateTimer) * ExploreDto.WalkSpeed);
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        private void ChangeRotation()
        {
            var rotatedMove = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            ExploreDto.PlayerController.Rotate(Quaternion.LookRotation(rotatedMove));
        }

        private void ReduceStamina()
        {
            ExploreDto.Stamina.AddValue(ExploreDto.RunStaminaReduce * Time.deltaTime);
        }

        public override void OnStateExit()
        {
            ExploreDto.CharacterAnimator.SetBool(IsRunning, false);
            ExploreDto.CharacterAnimator.SetBool(IsWalking, false);
            
            ExploreDto.SwitchStaminaRecovery(true);
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (IsJumping()) newMainCharacterState = Factory.Jump();
            else if (IsOutOfStamina()) newMainCharacterState = Factory.Walk();
            else if (IsFalling()) newMainCharacterState = Factory.Fall();
            else if (IsStill()) newMainCharacterState = Factory.Stop();
            else if (IsCrouching()) newMainCharacterState = Factory.Slide();
            else if (!IsSprinting()) newMainCharacterState = Factory.Walk();

            return newMainCharacterState != null;
        }

        private bool IsJumping()
        {
            return ExploreDto.InputProcessor.GetIsJumpPressed();
        }

        private bool IsOutOfStamina()
        {
            return ExploreDto.Stamina.StaminaPercentage == 0;
        }

        private bool IsFalling()
        {
            return !ExploreDto.PlayerController.IsGrounded;
        }

        private bool IsStill()
        {
            var currentInput = ExploreDto.InputProcessor.GetCurrentInput();
           return currentInput == Vector2.zero;
        }

        private bool IsCrouching()
        {
            return ExploreDto.InputProcessor.GetIsCrouchPressed();
        }

        private bool IsSprinting()
        {
            return ExploreDto.InputProcessor.GetIsSprintPressed();
        }
    }
}