namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public abstract class SubState<TFactory, TReturn> : State<TFactory, TReturn>
        where TFactory : SubStateFactory
    {
        protected SubState(TFactory factory) : base(factory){}
    }
}