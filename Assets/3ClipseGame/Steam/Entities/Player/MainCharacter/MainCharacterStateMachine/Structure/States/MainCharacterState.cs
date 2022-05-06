using System;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States
{
    public abstract class MainCharacterState : IStateMachine
    {
        #region Initialization

        public float StateTimer { get; private set; }
        public event Action<MainCharacterSubState, MainCharacterSubState> SwitchingSubState; 

        protected readonly MainCharacterStateMachine Context;
        protected readonly MainCharacterStateFactory Factory;

        protected MainCharacterState(MainCharacterStateMachine context, MainCharacterStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        #endregion

        #region AbstractMethods

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();

        public abstract bool TrySwitchState(out MainCharacterState newMainCharacterState);

        #endregion

        #region ProtectedMethods

        protected void AddTime(float deltaTime) => StateTimer += deltaTime;
        protected void SwitchSubState(MainCharacterSubState current, MainCharacterSubState next) => SwitchingSubState?.Invoke(current, next);

        #endregion
    }
}
