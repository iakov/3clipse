using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledWalkSubState : AnimalSubState
    {
        public ControlledWalkSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory)
            => _factory = (ControlledSubStatesFactory)factory;
        
        private ControlledSubStatesFactory _factory;

        public override void OnStateEnter(){}
        public override void OnStateUpdate()
        {
            var rawMoveVector = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var moveVector = rawMoveVector * Context.WalkSpeed;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputHandler.CurrentInput == Vector2.zero) newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }
    }
}
