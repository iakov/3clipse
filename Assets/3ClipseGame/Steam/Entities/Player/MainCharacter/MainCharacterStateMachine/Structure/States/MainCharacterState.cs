using System;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States
{
    public abstract class MainCharacterState : IStateMachine
    {
        #region Initialization

        protected float StateTimer { get; private set; }
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
        public virtual void OnStateUpdate() => AddTime(Time.deltaTime);
        public abstract void OnStateExit();

        public abstract bool TrySwitchState(out MainCharacterState newMainCharacterState);

        #endregion

        #region ProtectedMethods

        private void AddTime(float deltaTime) => StateTimer += deltaTime;
        protected void SwitchSubState(MainCharacterSubState current, MainCharacterSubState next) => SwitchingSubState?.Invoke(current, next);

        #endregion
    }
}
