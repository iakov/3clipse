namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public abstract class UncontrolledSubState : AnimalSubState<UncontrolledSubStatesFactory>
    {
        protected UncontrolledSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory){}

        public abstract override void OnStateEnter();

        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out AnimalSubState<UncontrolledSubStatesFactory> newSubState);
    }
}