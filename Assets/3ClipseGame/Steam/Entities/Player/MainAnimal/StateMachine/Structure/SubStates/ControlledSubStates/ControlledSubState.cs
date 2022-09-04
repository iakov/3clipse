namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public abstract class ControlledSubState : AnimalSubState
    {
        #region Initialization

        protected ControlledSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context)
            => Factory = factory;

        protected ControlledSubStatesFactory Factory;

        #endregion

        #region SubStateMethods

        public abstract override void OnStateEnter();

        public abstract override void OnStateUpdate();

        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out AnimalSubState newSubState);

        #endregion
    }
}