using System;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States
{
    public abstract class AnimalState : IStateMachine
    {
        #region Initialization

        public float StateTimer { get; private set; }
        public event Action<AnimalSubState, AnimalSubState> SwitchingSubState; 

        protected readonly MainAnimalStateMachine Context;
        protected readonly AnimalStateFactory Factory;

        protected AnimalState(MainAnimalStateMachine context, AnimalStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        #endregion

        #region AbstractMethods

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();

        public abstract bool TrySwitchState(out AnimalState newAnimalState);

        #endregion

        #region ProtectedMethods

        protected void AddTime(float deltaTime) => StateTimer += deltaTime;
        protected void SwitchSubState(AnimalSubState current, AnimalSubState next) => SwitchingSubState?.Invoke(current, next);

        #endregion
    }
}
