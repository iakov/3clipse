using System;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States
{
    public abstract class State
    {

        #region Initialization

        public float StateTimer { get; private set; }
        public event Action<SubState, SubState> SwitchingSubState; 

        protected readonly PlayerStateMachine Context;
        protected readonly StateFactory Factory;

        protected State(PlayerStateMachine context, StateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        #endregion

        #region AbstractMethods

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();

        public abstract bool TrySwitchState(out State newState);

        #endregion

        #region ProtectedMethods

        protected void AddTime(float deltaTime) => StateTimer += deltaTime;
        protected void SwitchSubState(SubState current, SubState next) => SwitchingSubState?.Invoke(current, next);

        #endregion
    }
}
