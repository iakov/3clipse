namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States
{
    public class ExploreState : State
    {
        public ExploreState(PlayerStateMachine context, StateFactory factory) : base(context, factory){}

        public override void OnStateEnter(){}
        public override void OnStateUpdate(){}
        public override void OnStateExit(){}

        public override bool TrySwitchState(out State newState)
        {
            newState = null;
            return false;
        }
    }
}
