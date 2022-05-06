namespace _3ClipseGame.Steam.Entities.Player
{
    public interface IStateMachine
    {
        public void OnStateEnter();
        public void OnStateUpdate();
        public void OnStateExit();
    }
}
