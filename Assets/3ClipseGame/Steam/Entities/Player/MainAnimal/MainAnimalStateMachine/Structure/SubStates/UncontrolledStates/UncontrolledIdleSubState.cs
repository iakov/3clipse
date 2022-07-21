using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledIdleSubState : AnimalSubState
    {
        #region Initialize

        public UncontrolledIdleSubState(MainAnimalStateMachine context, AnimalSubStateFactory factory) : base(context, factory) 
            => _factory = (UncontrolledSubStatesFactory) factory;

        private UncontrolledSubStatesFactory _factory;
            
        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
            Context.AnimalAgent.enabled = false;
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            
            Context.AnimalTransform.LookAt(Context.MainCharacterTransform);
        }

        public override void OnStateExit()
        {
            Context.CurrentTarget = Context.PossibleFollowTargets[Random.Range(0, Context.PossibleFollowTargets.Length)];
            Context.AnimalAgent.enabled = true;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position) > Context.FollowWalkDistance)
                newAnimalState = _factory.Walk();
            
            else if (StateTimer > Context.WaitTime) newAnimalState = _factory.Entertain();

            return newAnimalState != null;
        }
        
        #endregion
    }
}