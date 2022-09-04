using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public abstract class UncontrolledSubState : AnimalSubState
    {
        #region Initialization
        
        protected UncontrolledSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context)
            => Factory = factory;
        
        protected UncontrolledSubStatesFactory Factory;

        #endregion

        #region AbstractMethods

        public abstract override void OnStateEnter();

        public abstract override void OnStateUpdate();

        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out AnimalSubState newSubState);

        #endregion
    }
}