using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates
{
    public abstract class MainCharacterSubStateFactory : MainCharacterStateFactory
    {
        #region Initialization

        protected MainCharacterSubStateFactory(MainCharacterStateMachine context) : base(context){}

        #endregion
    }
}

