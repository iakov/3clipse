using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public abstract class AnimalState : IStateMachine
    {
        #region Initialization

        protected float StateTimer { get; private set; }

        protected readonly MainAnimalStateMachine Context;
        protected readonly AnimalStateFactory Factory;
        protected AnimalSubState CurrentSubState;

        protected AnimalState(MainAnimalStateMachine context, AnimalStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        #endregion

        #region AbstractMethods

        public virtual void OnStateEnter()
        {
            CurrentSubState.OnStateEnter();
        }

        public virtual void OnStateUpdate()
        {
            StateTimer += Time.deltaTime;
            
            if (CurrentSubState.TrySwitchState(out var newState)) SwitchSubState(newState);
            CurrentSubState.OnStateUpdate();
        }

        public virtual void OnStateExit()
        {
            CurrentSubState.OnStateExit();
        }
        
        public abstract bool TrySwitchState(out AnimalState newAnimalState);

        #endregion

        #region ProtectedMethods

        protected void SwitchSubState(AnimalSubState newAnimalSubState)
        {
            CurrentSubState.OnStateExit();
            CurrentSubState = newAnimalSubState;
            CurrentSubState.OnStateEnter();
        }

        #endregion
    }
}
