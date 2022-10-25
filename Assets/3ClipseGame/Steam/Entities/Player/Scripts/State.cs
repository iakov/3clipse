using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public abstract class State<TFactory, TReturn> 
        where TFactory : StateFactory
    {
        protected State(TFactory factory)
        {
            Factory = factory;
        }

        protected TFactory Factory { get; }
        protected float StateTimer { get; private set; }

        public abstract void OnStateEnter();
        public virtual void OnStateUpdate() => AddTime(Time.deltaTime);
        public abstract void OnStateExit();
        public abstract bool TrySwitchState(out TReturn newState);
        
        private void AddTime(float deltaTime) => StateTimer += deltaTime;
    }
}
