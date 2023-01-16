namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Scripts
{
    public abstract class SubState<TFactory, TReturn> : State<TFactory, TReturn>
        where TFactory : SubStateFactory
    {
        protected SubState(TFactory factory) : base(factory){}

        public override bool TrySwitchState(out TReturn newState)
        {
            return TrySwitch(out newState);
        }
    }
}