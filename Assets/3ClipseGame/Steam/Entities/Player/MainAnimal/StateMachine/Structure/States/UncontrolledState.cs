using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public class UncontrolledState : AnimalState
    {
        public UncontrolledState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        private UncontrolledSubStatesFactory _subStateFactory;
        private AnimalSubState<UncontrolledSubStatesFactory> _currentSubState;
        
        private int _framesFromSwitch;

        public override void OnStateEnter()
        {
            _subStateFactory = new UncontrolledSubStatesFactory(Context);
            _currentSubState = _subStateFactory.Idle();

            Context.AnimalAgent.enabled = true;
            Context.AnimalController.enabled = false;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (_currentSubState.TrySwitchState(out var newSubState)) SwitchSubState(newSubState);
            _currentSubState.OnStateUpdate();
            
            _framesFromSwitch++;
        }

        public override void OnStateExit()
        {
            Context.AnimalAgent.enabled = false;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputProcessor.GetIsSwitched() && _framesFromSwitch >= 2) newAnimalState = Factory.ControlledState();

            return newAnimalState != null;
        }

        private void SwitchSubState(AnimalSubState<UncontrolledSubStatesFactory> newSubState)
        {
            _currentSubState.OnStateExit();
            newSubState.OnStateEnter();
            _currentSubState = newSubState;
        }
    }
}
