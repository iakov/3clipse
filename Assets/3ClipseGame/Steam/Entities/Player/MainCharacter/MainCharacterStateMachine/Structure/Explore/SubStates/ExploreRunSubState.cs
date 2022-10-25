using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates
{
    public class ExploreRunSubState : MainCharacterExploreSubState
    {
        public ExploreRunSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        private float _timeToMaximumSpeed;
        
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void OnStateEnter()
        {
            ExploreDto.CharacterAnimator.SetBool(IsRunning, true);
            ExploreDto.CharacterAnimator.SetBool(IsWalking, true);
            
            _timeToMaximumSpeed = ExploreDto.RunModifierCurve.keys[ExploreDto.RunModifierCurve.length - 1].time;
            ExploreDto.Stamina.IsRecovering = false;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            Move();
            Rotate();
            ReduceStamina();
        }

        public override void OnStateExit()
        {
            ExploreDto.CharacterAnimator.SetBool(IsRunning, false);
            ExploreDto.CharacterAnimator.SetBool(IsWalking, false);
            
            ExploreDto.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (ExploreDto.InputProcessor.GetIsJumpPressed()) newMainCharacterState = Factory.Jump();
            else if (ExploreDto.Stamina.StaminaPercentage == 0) newMainCharacterState = Factory.Walk();
            else if (!ExploreDto.PlayerController.IsGrounded && !Physics.Raycast(ExploreDto.Transform.position, Vector3.down,
                    ExploreDto.PlayerController.Radius)) newMainCharacterState = Factory.Fall();
            else if (ExploreDto.InputProcessor.GetCurrentInput() == Vector2.zero) newMainCharacterState = Factory.Stop();
            else if (ExploreDto.InputProcessor.GetIsCrouchPressed()) newMainCharacterState = Factory.Slide();
            else if (!ExploreDto.InputProcessor.GetIsSprintPressed()) newMainCharacterState = Factory.Walk();

            return newMainCharacterState != null;
        }

        private void Move()
        {
            var rawMoveVector = new Vector3(ExploreDto.InputProcessor.GetCurrentInput().x, 0f, ExploreDto.InputProcessor.GetCurrentInput().y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * (ExploreDto.RunModifierCurve.Evaluate(currentEvaluateTime) * ExploreDto.WalkSpeed);
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        private void Rotate()
        {
            var rotatedMove = ExploreDto.PlayerMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            ExploreDto.PlayerController.Rotate(Quaternion.LookRotation(rotatedMove));
        }

        private void ReduceStamina()
        {
            ExploreDto.Stamina.AddValue(ExploreDto.RunStaminaReduce * Time.deltaTime);
        }
    }
}