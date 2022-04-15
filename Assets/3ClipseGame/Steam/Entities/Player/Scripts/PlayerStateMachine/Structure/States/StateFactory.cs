namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States
{
    public class StateFactory
    {
        public StateFactory(PlayerStateMachine context) => _context = context;
        protected PlayerStateMachine _context;

        public State ExploreState() => new ExploreState(_context, this);
    }
}
