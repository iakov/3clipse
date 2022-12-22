using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreIdleSubState : MainCharacterExploreSubState
    {
        public ExploreIdleSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}
        
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Angle = Animator.StringToHash("Angle");

        #region StateMethods

        public override void OnStateEnter()
        {
            ExploreDto.SaveLastMove(true);
            SpeedDown();
            SetZeroAngle();
            ExploreDto.CharacterAnimator.SetFloat(Angle, 0f, ExploreDto.WalkAngleDampTime, Time.deltaTime);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateExit()
        {
        }
        
        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (!IsStill()) newMainCharacterState = Factory.Walk();

            return newMainCharacterState != null;
        }

        #endregion

        private void SpeedDown()
        {
            ExploreDto.CharacterAnimator.SetFloat(Speed, 0f);
        }
        
        private void SetZeroAngle()
        {
            var dampTime = ExploreDto.WalkAngleDampTime;
            ExploreDto.CharacterAnimator.SetFloat(Angle, 0f, dampTime, Time.deltaTime);
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