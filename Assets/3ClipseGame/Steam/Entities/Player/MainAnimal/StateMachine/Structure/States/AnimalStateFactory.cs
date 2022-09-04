namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public class AnimalStateFactory
    {
        #region Initialization

        public AnimalStateFactory(MainAnimalStateMachine context) => Context = context;
        
        protected readonly MainAnimalStateMachine Context;

        #endregion

        #region Methods

        public AnimalState ControlledState() => new ControlledState(Context, this);
        public AnimalState UncontrolledState() => new UncontrolledState(Context, this);

        #endregion
    }
}
