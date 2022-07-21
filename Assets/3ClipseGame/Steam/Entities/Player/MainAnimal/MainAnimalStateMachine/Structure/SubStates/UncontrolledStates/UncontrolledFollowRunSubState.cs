using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledFollowRunSubState : AnimalSubState
    {
        public UncontrolledFollowRunSubState(MainAnimalStateMachine context, AnimalSubStateFactory factory) : base(context, factory)
            => _factory = (UncontrolledSubStatesFactory) factory;
        
        private float _distanceBetweenPlayerAndAnimal;
        private UncontrolledSubStatesFactory _factory;

        public override void OnStateEnter(){}
        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            Context.AnimalAgent.SetDestination(Context.CurrentTarget.position);
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position);
            Context.AnimalAgent.speed = Context.FollowRunSpeed.Evaluate(_distanceBetweenPlayerAndAnimal);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal < Context.FollowRunDistance) newAnimalState = _factory.Walk();
                
            return newAnimalState != null;
        }
    }
}