using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledFollowRunSubState : UncontrolledSubState
    {
        public UncontrolledFollowRunSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory)
            => Factory = factory;

        protected UncontrolledSubStatesFactory Factory;
        
        private float _distanceBetweenPlayerAndAnimal;

        public override void OnStateEnter()
        {
            
        }
        
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            if (Context.AnimalAgent.isOnNavMesh) Context.AnimalAgent.SetDestination(Context.CurrentTarget.position);
            
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position);
            Context.AnimalAgent.speed = Context.FollowRunSpeed.Evaluate(_distanceBetweenPlayerAndAnimal);
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out AnimalSubState<UncontrolledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal < Context.FollowRunDistance) newAnimalState = Factory.Walk();
                
            return newAnimalState != null;
        }
    }
}