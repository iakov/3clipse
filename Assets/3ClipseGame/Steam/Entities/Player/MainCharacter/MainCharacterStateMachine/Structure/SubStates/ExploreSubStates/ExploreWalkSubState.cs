using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreWalkSubState : MainCharacterSubState
    {
        #region Initialization

        public ExploreWalkSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private bool _isJumped;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.CharacterAnimator.SetBool(IsWalking, true);
            Context.InputHandler.JumpPressed += OnJumpPressed;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            Move();
            Rotate();
        }

        public override void OnStateExit()
        {
            Context.CharacterAnimator.SetBool(IsWalking, false);
            Context.InputHandler.JumpPressed -= OnJumpPressed;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (!Context.PlayerController.IsGrounded && !Context.PlayerController.IsGrounded) newMainCharacterState = _factory.Fall();
            else if (_isJumped) newMainCharacterState = _factory.Jump();
            else if (Context.InputHandler.GetCurrentInput() == Vector2.zero) newMainCharacterState = _factory.Stop();
            else if (Context.InputHandler.GetIsRunPressed() && Context.Stamina.StaminaPercentage > Context.MinRunEntryStamina) newMainCharacterState = _factory.Run();
            else if (Context.InputHandler.GetIsCrouchPressed()) newMainCharacterState = _factory.Crouch();
            
            return newMainCharacterState != null;
        }

        #endregion
        
        private void Move()
        {
            var rawMoveVector = new Vector3(Context.InputHandler.GetCurrentInput().x, 0f, Context.InputHandler.GetCurrentInput().y);
            var moveVector = rawMoveVector * Context.WalkSpeed;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        private void Rotate()
        {
            var rotatedMove = Context.PlayerMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            
            Context.PlayerController.Rotate(Quaternion.LookRotation(rotatedMove));
        }

        private void OnJumpPressed() => _isJumped = true;
    }
}