using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreRunSubState : MainCharacterSubState
    {
        #region Initialization

        public ExploreRunSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private float _timeToMaximumSpeed;
        private bool _isJumped;
        
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.CharacterAnimator.SetBool(IsRunning, true);
            Context.CharacterAnimator.SetBool(IsWalking, true);
            
            _timeToMaximumSpeed = Context.RunModifierCurve.keys[Context.RunModifierCurve.length - 1].time;
            Context.Stamina.IsRecovering = false;

            Context.InputHandler.JumpPressed += OnJumpPressed;
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
            Context.CharacterAnimator.SetBool(IsRunning, false);
            Context.CharacterAnimator.SetBool(IsWalking, false);
            
            Context.Stamina.IsRecovering = true;
            
            Context.InputHandler.JumpPressed += OnJumpPressed;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (_isJumped) newMainCharacterState = _factory.Jump();
            else if (Context.Stamina.StaminaPercentage == 0) newMainCharacterState = _factory.Walk();
            else if (!Context.PlayerController.IsGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.Radius)) newMainCharacterState = _factory.Fall();
            else if (Context.InputHandler.GetCurrentInput() == Vector2.zero) newMainCharacterState = _factory.Stop();
            else if (Context.InputHandler.GetIsCrouchPressed()) newMainCharacterState = _factory.Slide();
            else if (!Context.InputHandler.GetIsRunPressed()) newMainCharacterState = _factory.Walk();

            return newMainCharacterState != null;
        }

        private void Move()
        {
            var rawMoveVector = new Vector3(Context.InputHandler.GetCurrentInput().x, 0f, Context.InputHandler.GetCurrentInput().y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * (Context.RunModifierCurve.Evaluate(currentEvaluateTime) * Context.WalkSpeed);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        private void Rotate()
        {
            var rotatedMove = Context.PlayerMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            Context.PlayerController.Rotate(Quaternion.LookRotation(rotatedMove));
        }

        private void ReduceStamina()
        {
            Context.Stamina.AddValue(Context.RunStaminaReduce * Time.deltaTime);
        }

        private void OnJumpPressed() => _isJumped = true;

        #endregion
    }
}