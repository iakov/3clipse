using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledRunSubState : ControlledSubState
    {
        #region Initialization

        public ControlledRunSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        private float _timeToMaximumSpeed;
        private bool _isJumped;
        
        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.Stamina.IsRecovering = false;
            _timeToMaximumSpeed = Context.RunSpeed.keys[Context.RunSpeed.length - 1].time;
            Context.InputHandler.JumpPressed += OnJumpPressed;
        }
        
        public override void OnStateUpdate()
        {
            StateTimer += Time.deltaTime;
            
            var rawMoveVector = new Vector3(Context.InputHandler.GetCurrentInput().x, 0f, Context.InputHandler.GetCurrentInput().y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * (Context.RunSpeed.Evaluate(currentEvaluateTime) * Context.WalkSpeed);
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
            
            Context.Stamina.AddValue(Context.RunStaminaReduce * Time.deltaTime);
        }

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
            Context.InputHandler.JumpPressed -= OnJumpPressed;
        }

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;
            
            if (_isJumped) newAnimalState = Factory.Jump();
            else if (Context.Stamina.StaminaPercentage == 0) newAnimalState = Factory.Walk();
            else if (!Context.InputHandler.GetIsRunPressed()) newAnimalState = Factory.Walk();
            else if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                         Context.AnimalController.Radius)) newAnimalState = Factory.Fall();
            else if (Context.InputHandler.GetCurrentInput() == Vector2.zero) newAnimalState = Factory.Stop();
            else if (Context.InputHandler.GetIsCrouchPressed()) newAnimalState = Factory.Crouch();

            return newAnimalState != null;
        }
        
        #endregion

        private void OnJumpPressed() => _isJumped = true;
    }
}