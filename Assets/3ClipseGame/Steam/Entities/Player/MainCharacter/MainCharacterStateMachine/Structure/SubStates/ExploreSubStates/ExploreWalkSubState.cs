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
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int DeltaRotation = Animator.StringToHash("DeltaRotation");

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.CharacterAnimator.SetBool(IsWalking, true);
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
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (!Context.PlayerController.IsGrounded && !Context.PlayerController.IsGrounded) newMainCharacterState = _factory.Fall();
            else if (Context.InputHandler.IsJumpPressed) newMainCharacterState = _factory.Jump();
            else if (Context.InputHandler.CurrentInput == Vector2.zero) newMainCharacterState = _factory.Stop();
            else if (Context.InputHandler.IsRunPressed && Context.Stamina.StaminaValue / Context.Stamina.staminaMax > 0.3f) newMainCharacterState = _factory.Run();
            else if (Context.InputHandler.IsCrouchPressed) newMainCharacterState = _factory.Crouch();
            
            return newMainCharacterState != null;
        }

        private void Move()
        {
            var rawMoveVector = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var moveVector = rawMoveVector * Context.WalkSpeed;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        private void Rotate()
        {
            var rotatedMove = Context.PlayerMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            
            Context.PlayerController.Rotate(Quaternion.LookRotation(rotatedMove));
        }

        #endregion
    }
}