using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledCrouchSubState : ControlledSubState
    {
        public ControlledCrouchSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}
        
        public override void OnStateEnter(){}

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            var rawInput = new Vector3(Context.InputProcessor.GetCurrentInput().x, 0f, Context.InputProcessor.GetCurrentInput().y);
            var moveVector = rawInput * Context.CrouchSpeedModifier;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateWithCamera);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;
            
            if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                    Context.AnimalController.Radius)) newAnimalState = Factory.Fall();
            else if (!Context.InputProcessor.GetIsCrouchPressed()) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }
    }
}