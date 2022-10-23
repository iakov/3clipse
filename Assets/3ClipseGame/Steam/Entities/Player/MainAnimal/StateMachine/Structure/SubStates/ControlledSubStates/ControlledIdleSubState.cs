using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledIdleSubState : ControlledSubState
    {
        public ControlledIdleSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputProcessor.GetIsJumpPressed()) newAnimalState = Factory.Jump();
            else if (Context.InputProcessor.GetCurrentInput() != Vector2.zero) newAnimalState = Factory.Walk();
            else if (!Context.AnimalController.IsGrounded) newAnimalState = Factory.Fall();
            else if (Context.InputProcessor.GetIsCrouchPressed()) newAnimalState = Factory.Crouch();

            return newAnimalState != null;
        }
    }
}
