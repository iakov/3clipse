using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates
{
    public abstract class SubStateFactory : StateFactory
    {
        protected SubStateFactory(PlayerStateMachine context) : base(context){}
    }
}

