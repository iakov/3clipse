namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States
{
    public abstract class State
    {
        public float StateTimer { get; private set; }
        
        protected readonly PlayerStateMachine Context;
        protected readonly StateFactory Factory;

        protected State(PlayerStateMachine context, StateFactory factory)
        {
            Context = context;
            Factory = factory;
        }
        
        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();

        public abstract bool TrySwitchState(out State newState);
        protected void AddTime(float deltaTime) => StateTimer += deltaTime;
    }
}
