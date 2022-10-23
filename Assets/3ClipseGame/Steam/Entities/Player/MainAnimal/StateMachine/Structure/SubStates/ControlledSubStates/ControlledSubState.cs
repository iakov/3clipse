namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public abstract class ControlledSubState : AnimalSubState<ControlledSubStatesFactory>
    {
        protected ControlledSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        public abstract override void OnStateEnter();

        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newSubState);
    }
}