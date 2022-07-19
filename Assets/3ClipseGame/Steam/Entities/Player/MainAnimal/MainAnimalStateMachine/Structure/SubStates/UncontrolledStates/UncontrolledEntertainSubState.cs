using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledEntertainSubState : AnimalSubState
    {
        #region Initialize

        public UncontrolledEntertainSubState(MainAnimalStateMachine context, AnimalSubStateFactory factory) : base(context, factory) 
            => _factory = (UncontrolledSubStatesFactory) factory;

        private UncontrolledSubStatesFactory _factory;
            
        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
        }
        
        public override void OnStateUpdate(){}
        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.MainCharacterTransform.gameObject.GetComponent<CharacterController>().Velocity != Vector3.zero)
                newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }

        #endregion
    }
}