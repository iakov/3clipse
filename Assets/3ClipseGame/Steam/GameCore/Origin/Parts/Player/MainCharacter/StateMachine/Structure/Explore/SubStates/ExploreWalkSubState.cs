using _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreWalkSubState : MainCharacterExploreSubState
    {
        public ExploreWalkSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory){}

        private static readonly int Angle = Animator.StringToHash("Angle");
        private static readonly int Speed = Animator.StringToHash("Speed");

        #region Work
        
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
            if(angle >= 170f || angle <= -170f) Debug.Log("Full turn");
            if((angle >= 120f && angle < 170f) || ( angle <= -120f && angle > -170f)) Debug.Log("135 turn");
            if((angle >= 80f && angle < 120f) || ( angle <= -80f && angle > -120f)) Debug.Log("90 turn");

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

        #endregion

        #region Switch

        private float _stillTime;

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (IsStill()) newMainCharacterState = Factory.Idle();
            
            return newMainCharacterState != null;
        }

        private bool IsStill()
        {
            var isInputStill = ExploreDto.InputProcessor.GetCurrentInput() == Vector2.zero;
            
            if (!isInputStill) _stillTime = 0f;
            else _stillTime += Time.deltaTime;
            
            return _stillTime >= ExploreDto.ToIdleDampTime;
        }

        #endregion
    }
}