namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates
{
    public abstract class AnimalSubState : IStateMachine
    {
        protected AnimalSubState(MainAnimalStateMachine context)
            => Context = context;

        protected MainAnimalStateMachine Context;
        protected float StateTimer;

        public abstract void OnStateEnter();

        public abstract void OnStateUpdate();

        public abstract void OnStateExit();

        public abstract bool TrySwitchState(out AnimalSubState newSubState);
    }
}
