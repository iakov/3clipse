namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States
{
    public class MainCharacterStateFactory
    {
        #region Initialization

        public MainCharacterStateFactory(MainCharacterStateMachine context) => Context = context;
        protected readonly MainCharacterStateMachine Context;

        #endregion

        #region Methods

        public MainCharacterState ExploreState() => new ExploreMainCharacterState(Context, this);
        public MainCharacterState AnimalControlState() => new AnimalControlState(Context, this);

        #endregion
    }
}
