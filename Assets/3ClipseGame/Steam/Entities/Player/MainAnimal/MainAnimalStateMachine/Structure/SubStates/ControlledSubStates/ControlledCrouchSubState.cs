using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledCrouchSubState : AnimalSubState
    {
        public ControlledCrouchSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory) 
            => _factory = (ControlledSubStatesFactory) factory;
        
        private ControlledSubStatesFactory _factory;

        public override void OnStateEnter(){}

        public override void OnStateUpdate()
        {
            var rawInput = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var moveVector = rawInput * Context.CrouchSpeedModifier;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateWithCamera);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;
            
            if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                    Context.AnimalController.Radius)) newAnimalState = _factory.Fall();
            else if (!Context.InputHandler.IsCrouchPressed) newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }
    }
}