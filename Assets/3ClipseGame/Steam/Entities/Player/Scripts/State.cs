using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public abstract class State<T1, T2, T3> where T1 : State<T1, T2, T3> where T2 : StateFactory<T3> where T3 : StateMachine
    {
        protected State(T3 context, T2 factory)
        {
            Factory = factory;
            Context = context;
        }

        protected T2 Factory { get; }
        protected T3 Context { get; }
        protected float StateTimer { get; private set; }

        public abstract void OnStateEnter();
        public virtual void OnStateUpdate() => AddTime(Time.deltaTime);
        public abstract void OnStateExit();
        public abstract bool TrySwitchState(out T1 newState);
        
        private void AddTime(float deltaTime) => StateTimer += deltaTime;
    }
}
