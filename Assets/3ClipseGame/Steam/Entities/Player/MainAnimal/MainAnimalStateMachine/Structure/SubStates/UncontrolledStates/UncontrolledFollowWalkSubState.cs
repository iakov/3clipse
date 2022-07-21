using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledFollowWalkSubState : AnimalSubState
    {
<<<<<<< HEAD
        public UncontrolledFollowWalkSubState(MainAnimalStateMachine context, AnimalSubStateFactory factory) : base(context, factory){}
=======
        public UncontrolledFollowWalkSubState(MainAnimalStateMachine context, AnimalSubStateFactory factory) : base(context, factory)
            => _factory = (UncontrolledSubStatesFactory) factory;

        private float _distanceBetweenPlayerAndAnimal;
        private UncontrolledSubStatesFactory _factory;
>>>>>>> 60b0e68092cb23e487f64654f07b63b41a792a22

        public override void OnStateEnter(){}

        public override void OnStateUpdate()
        {
            Context.AnimalAgent.SetDestination(Context.CurrentTarget.position);
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position);
            Context.AnimalAgent.speed = Context.FollowWalkSpeed.Evaluate(_distanceBetweenPlayerAndAnimal);
        }
        public override void OnStateExit(){}
        
        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.FollowRunDistance) newAnimalState = _factory.Run();
            else if (_distanceBetweenPlayerAndAnimal < Context.StopWalkDistance) newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }
    }
}