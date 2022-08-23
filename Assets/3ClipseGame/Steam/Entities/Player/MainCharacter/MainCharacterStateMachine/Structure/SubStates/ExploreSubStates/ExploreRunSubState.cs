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
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            var rawMoveVector = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * (Context.RunModifierCurve.Evaluate(currentEvaluateTime) * Context.WalkSpeed);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
            
            Context.Stamina.AddValue(Context.RunStaminaReduce * Time.deltaTime);
        }

        public override void OnStateExit()
        {
            Context.CharacterAnimator.SetBool(IsRunning, false);
            Context.CharacterAnimator.SetBool(IsWalking, false);
            
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (Context.InputHandler.IsJumpPressed) newMainCharacterState = _factory.Jump();
            else if (Context.Stamina.StaminaValue == 0) newMainCharacterState = _factory.Walk();
            else if (!Context.PlayerController.IsGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.Radius)) newMainCharacterState = _factory.Fall();
            else if (Context.InputHandler.CurrentInput == Vector2.zero) newMainCharacterState = _factory.Stop();
            else if (Context.InputHandler.IsCrouchPressed) newMainCharacterState = _factory.Slide();
            else if (!Context.InputHandler.IsRunPressed) newMainCharacterState = _factory.Walk();

            return newMainCharacterState != null;
        }

        #endregion
    }
}