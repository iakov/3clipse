using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreWalkSubState : MainCharacterExploreSubState
    {
        public ExploreWalkSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory){}

        private static readonly int Angle = Animator.StringToHash("Angle");
        private static readonly int Speed = Animator.StringToHash("Speed");

        #region StateMethods
        
        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            SetAngleWithDamping();
            SetSpeed();
        }
        
        public override void OnStateExit()
        {
        }
        
        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (ExploreDto.InputProcessor.GetCurrentInput() == Vector2.zero) newMainCharacterState = Factory.Idle();
            
            return newMainCharacterState != null;
        }

        #endregion
        
        private void SetAngleWithDamping()
        {
            var angleDampTime = ExploreDto.WalkAngleDampTime;
            var angle = GetAngle();
            ExploreDto.CharacterAnimator.SetFloat(Angle, angle, angleDampTime, Time.deltaTime);
        }

        private float GetAngle()
        {
            var inputRaw = ExploreDto.InputProcessor.GetCurrentInput();
            var inputVector = new Vector3(inputRaw.x, 0f, inputRaw.y);
            var forwardVector = ExploreDto.PlayerCollider.transform.forward;
            var rotatedInputVector = ExploreDto.PlayerMover.RotateWithCamera(inputVector, MoveType.StateMove, RotationType.RotateOnBeginning);
            var angle = Vector3.SignedAngle(rotatedInputVector, forwardVector, Vector3.down);
            return angle;
        }

        private void SetSpeed()
        {
            var time = StateTimer;
            var maxTime = ExploreDto.TimeToMaxWalkSpeed;

            var progress = time / maxTime;
            progress = Mathf.Min(progress, 1f);
            var speed = progress * ExploreDto.MaxWalkSpeed;
            
            ExploreDto.CharacterAnimator.SetFloat(Speed, speed);
        }
    }
}