using _3ClipseGame.Steam.Core.GameSource;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public class ControlledState : AnimalState
    {
        #region Initialization

        public ControlledState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}
        
        private ControlledSubStatesFactory _subStateFactory;

        #endregion
        
        #region StateMethods

        public override void OnStateEnter()
        {
            _subStateFactory = new ControlledSubStatesFactory(Context);
            CurrentSubState = _subStateFactory.Idle();
            base.OnStateEnter();
           
            Context.AnimalAgent.enabled = false;
            Context.AnimalController.enabled = true;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            
            CurrentSubState.OnStateExit();
            Context.AnimalAgent.enabled = true;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.IsSwitching) newAnimalState = Factory.UncontrolledState();

            return newAnimalState != null;
        }

        #endregion
    }
}
