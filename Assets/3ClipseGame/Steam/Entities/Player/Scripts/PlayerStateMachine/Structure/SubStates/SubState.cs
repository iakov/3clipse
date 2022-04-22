using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;

namespace Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates
{
    public abstract class SubState : State
    {
        #region Initialization

        protected SubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}

        #endregion
    }
}
