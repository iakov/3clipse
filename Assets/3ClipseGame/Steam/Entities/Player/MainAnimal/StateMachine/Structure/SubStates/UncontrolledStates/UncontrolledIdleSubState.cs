using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledIdleSubState : UncontrolledSubState
    {
        #region Initialize

        public UncontrolledIdleSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory) {}

        private float _distanceBetweenPlayerAndAnimal;
            
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
            StateTimer += Time.deltaTime;
            
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position);
            Context.AnimalTransform.LookAt(Context.MainCharacterTransform);
        }

        public override void OnStateExit() => Context.CurrentTarget = Context.PossibleFollowTargets[Random.Range(0, Context.PossibleFollowTargets.Length)];

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.FollowWalkDistance)
                newAnimalState = Factory.Walk();
            else if (_distanceBetweenPlayerAndAnimal < Context.WalkBackDistance) newAnimalState = Factory.WalkBack();
            else if (StateTimer > Context.WaitTime) newAnimalState = Factory.Entertain();

            return newAnimalState != null;
        }
        
        #endregion
    }
}