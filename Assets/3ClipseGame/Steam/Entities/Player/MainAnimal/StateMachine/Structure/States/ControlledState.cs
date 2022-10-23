using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public class ControlledState : AnimalState
    {
        public ControlledState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}
        
        private ControlledSubStatesFactory _subStateFactory;
        private AnimalSubState<ControlledSubStatesFactory> _currentSubState;
        
        private int _framesFromSwitch;

        public override void OnStateEnter()
        {
            _subStateFactory = new ControlledSubStatesFactory(Context);
            _currentSubState = _subStateFactory.Idle();

            Context.AnimalAgent.enabled = false;
            Context.AnimalController.enabled = true;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (_currentSubState.TrySwitchState(out var newSubState)) SwitchState(newSubState);
            _currentSubState.OnStateUpdate();
            
            _framesFromSwitch++;
        }
        
        public override void OnStateExit()
        {
            _currentSubState.OnStateExit();
            Context.AnimalAgent.enabled = true;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputProcessor.GetIsSwitched() && _framesFromSwitch >= 2) newAnimalState = Factory.UncontrolledState();

            return newAnimalState != null;
        }

        private void SwitchState(AnimalSubState<ControlledSubStatesFactory> newState)
        {
            _currentSubState.OnStateExit();
            newState.OnStateEnter();
            _currentSubState = newState;
        }
    }
}
