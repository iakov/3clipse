using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledRunSubState : ControlledSubState
    {
        public ControlledRunSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        private float _timeToMaximumSpeed;

        public override void OnStateEnter()
        {
            Context.Stamina.IsRecovering = false;
            _timeToMaximumSpeed = Context.RunSpeed.keys[Context.RunSpeed.length - 1].time;
        }
        
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            var rawMoveVector = new Vector3(Context.InputProcessor.GetCurrentInput().x, 0f, Context.InputProcessor.GetCurrentInput().y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * (Context.RunSpeed.Evaluate(currentEvaluateTime) * Context.WalkSpeed);
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
            
            Context.Stamina.AddValue(Context.RunStaminaReduce * Time.deltaTime);
        }

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;
            
            if (Context.InputProcessor.GetIsJumpPressed()) newAnimalState = Factory.Jump();
            else if (Context.Stamina.StaminaPercentage == 0) newAnimalState = Factory.Walk();
            else if (!Context.InputProcessor.GetIsSprintPressed()) newAnimalState = Factory.Walk();
            else if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                         Context.AnimalController.Radius)) newAnimalState = Factory.Fall();
            else if (Context.InputProcessor.GetCurrentInput() == Vector2.zero) newAnimalState = Factory.Stop();
            else if (Context.InputProcessor.GetIsCrouchPressed()) newAnimalState = Factory.Crouch();

            return newAnimalState != null;
        }
    }
}