using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledEntertainSubState : UncontrolledSubState
    {
        #region Initialize

        public UncontrolledEntertainSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory){}

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
        }
        
        public override void OnStateUpdate(){}
        
        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (Context.MainCharacterController.Velocity != Vector3.zero) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }

        #endregion
    }
}