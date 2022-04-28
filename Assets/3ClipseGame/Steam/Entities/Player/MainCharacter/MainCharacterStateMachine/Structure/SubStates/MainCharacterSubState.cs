using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates
{
    public abstract class MainCharacterSubState : MainCharacterState
    {
        #region Initialization

        protected MainCharacterSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory){}

        #endregion
    }
}
