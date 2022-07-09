using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledIdleSubState : AnimalSubState
    {
        public UncontrolledIdleSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
        }
        
        public override void OnStateUpdate(){}
        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;
            
            //if Distance to MainCharacter more than number => Go to UcontrolledJumpSubstate

            return newAnimalState != null;
        }
    }
}