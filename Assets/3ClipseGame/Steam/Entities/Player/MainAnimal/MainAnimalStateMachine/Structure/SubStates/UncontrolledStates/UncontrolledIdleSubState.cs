using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledIdleSubState : AnimalSubState
    {
        #region Initialize

        public UncontrolledIdleSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

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
            
            Debug.Log(Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position));

            return newAnimalState != null;
        }

        #endregion
    }
}