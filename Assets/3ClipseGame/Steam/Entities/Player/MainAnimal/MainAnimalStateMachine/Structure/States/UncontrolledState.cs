using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States
{
    public class UncontrolledState : AnimalState
    {
        public UncontrolledState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        private AnimalSubState _currentSubState;
        private UncontrolledSubStatesFactory _subStateFactory;
        private Vector3 _currentTarget;

        public override void OnStateEnter()
        {
            _subStateFactory = new UncontrolledSubStatesFactory(Context);
            _currentSubState = _subStateFactory.Idle();
            _currentSubState.OnStateEnter();
            Context.AnimalAgent.enabled = true;
        }

        public override void OnStateUpdate()
        {
            if (_currentSubState.TrySwitchState(out var newState)) SwitchState((AnimalSubState) newState);
            _currentSubState.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            _currentSubState.OnStateExit();
            Context.AnimalAgent.enabled = false;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            return newAnimalState != null;
        }

        private void SwitchState(AnimalSubState newAnimalSubState)
        {
            SwitchSubState(_currentSubState, newAnimalSubState);
            _currentSubState.OnStateExit();
            _currentSubState = newAnimalSubState;
            _currentSubState.OnStateEnter();
        }

        private void RayCast()
        {
            float maxDistance = Context.WalkBackDistance;
            RaycastHit hit;

            // bool isHit = Physics.Raycast();
        }
    }
}
