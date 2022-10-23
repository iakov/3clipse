namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public abstract class StateFactory<T> where T : StateMachine
    {
        protected StateFactory(T context) => Context = context;
        protected T Context;
    }
}
