using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledEntertainSubState : UncontrolledSubState
    {
        public UncontrolledEntertainSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory){}

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalSubState<UncontrolledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (Context.MainCharacterController.Velocity != Vector3.zero) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }
    }
}