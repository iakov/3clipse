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

        private int _framesFromSwitch;

        public abstract void OnStateEnter();

        public virtual void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            _framesFromSwitch++;
        }

        public abstract void OnStateExit();

        public virtual bool TrySwitchState(out TReturn newState)
        {
            newState = default;

            return _framesFromSwitch >= 2 && TrySwitch(out newState);
        }

        protected abstract bool TrySwitch(out TReturn newState);
        
        private void AddTime(float deltaTime) => StateTimer += deltaTime;
    }
}
