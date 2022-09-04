using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledFollowWalkSubState : UncontrolledSubState
    {
        #region Initialization

        public UncontrolledFollowWalkSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory){}

        private float _distanceBetweenPlayerAndAnimal;

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {
            if (Context.AnimalAgent.isOnNavMesh) Context.AnimalAgent.SetDestination(Context.CurrentTarget.position);
            
            var position = Context.AnimalTransform.position;
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(position, Context.MainCharacterTransform.position);
            Context.AnimalAgent.speed = Context.FollowWalkSpeed.Evaluate(_distanceBetweenPlayerAndAnimal);
        }

        public override void OnStateExit()
        {
            
        }
        
        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.FollowRunDistance) newAnimalState = Factory.Run();
            else if (_distanceBetweenPlayerAndAnimal < Context.StopWalkDistance && !Context.AnimalAgent.isOnOffMeshLink) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }

        #endregion
    }
}