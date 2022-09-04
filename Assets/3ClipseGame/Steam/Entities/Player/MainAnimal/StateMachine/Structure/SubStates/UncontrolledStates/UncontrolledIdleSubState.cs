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

        private float _distanceBetweenPlayerAndAnimal;
        private UncontrolledSubStatesFactory _factory;
            
        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
            Context.AnimalAgent.enabled = false;
            Context.AnimalAgent.enabled = true;
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position);
            Context.AnimalTransform.LookAt(Context.MainCharacterTransform);
        }

        public override void OnStateExit() => Context.CurrentTarget = Context.PossibleFollowTargets[Random.Range(0, Context.PossibleFollowTargets.Length)];

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.FollowWalkDistance)
                newAnimalState = _factory.Walk();
            else if (_distanceBetweenPlayerAndAnimal < Context.WalkBackDistance) newAnimalState = _factory.WalkBack();
            else if (StateTimer > Context.WaitTime) newAnimalState = _factory.Entertain();

            return newAnimalState != null;
        }
        
        #endregion
    }
}