using System;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledFollowWalkSubState : AnimalSubState
    {
        public UncontrolledFollowWalkSubState(MainAnimalStateMachine context, AnimalSubStateFactory factory) : base(context, factory)
            => _factory = (UncontrolledSubStatesFactory) factory;

        private float _distanceBetweenPlayerAndAnimal;
        private UncontrolledSubStatesFactory _factory;

        public override void OnStateEnter(){}

        public override void OnStateUpdate()
        {
            if (Context.AnimalAgent.isOnNavMesh) Context.AnimalAgent.SetDestination(Context.CurrentTarget.position);
            
            var position = Context.AnimalTransform.position;
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(position, Context.MainCharacterTransform.position);
            Context.AnimalAgent.speed = Context.FollowWalkSpeed.Evaluate(_distanceBetweenPlayerAndAnimal);
        }
        public override void OnStateExit(){}
        
        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.FollowRunDistance) newAnimalState = _factory.Run();
            else if (_distanceBetweenPlayerAndAnimal < Context.StopWalkDistance && !Context.AnimalAgent.isOnOffMeshLink) newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }
    }
}