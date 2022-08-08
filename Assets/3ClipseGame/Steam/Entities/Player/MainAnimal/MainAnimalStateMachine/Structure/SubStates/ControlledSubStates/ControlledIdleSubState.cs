using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledIdleSubState : AnimalSubState
    {
        public ControlledIdleSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory)
            => _factory = (ControlledSubStatesFactory)factory;
        
        private ControlledSubStatesFactory _factory;

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.NoRotation);
        }

        public override void OnStateUpdate(){}

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputHandler.IsJumpPressed) newAnimalState = _factory.Jump();
            else if (Context.InputHandler.CurrentInput != Vector2.zero) newAnimalState = _factory.Walk();

            return newAnimalState != null;
        }
    }
}
