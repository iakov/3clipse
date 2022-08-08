using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledRunSubState : AnimalSubState
    {
        public ControlledRunSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context,
            factory) => _factory = (ControlledSubStatesFactory) factory;

        private ControlledSubStatesFactory _factory;
        private float _timeToMaximumSpeed;

        public override void OnStateEnter()
        {
            Context.Stamina.IsRecovering = false;
            _timeToMaximumSpeed = Context.RunSpeed.keys[Context.RunSpeed.length - 1].time;
        }
        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var rawMoveVector = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var currentEvaluateTime = StateTimer <= _timeToMaximumSpeed ? StateTimer : _timeToMaximumSpeed;
            var moveVector = rawMoveVector * (Context.RunSpeed.Evaluate(currentEvaluateTime) * Context.WalkSpeed);
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputHandler.IsJumpPressed) newAnimalState = _factory.Jump();
            else if (Context.Stamina.StaminaValue == 0) newAnimalState = _factory.Walk();
            else if (!Context.InputHandler.IsRunPressed) newAnimalState = _factory.Walk();
            else if (Context.InputHandler.CurrentInput == Vector2.zero) newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }
    }
}