namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreFallSubState : MainCharacterExploreSubState
    {
        public ExploreFallSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        public override void OnStateEnter()
        {
            ExploreDto.SwitchStaminaRecovery(false);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            ExploreDto.SwitchStaminaRecovery(true);
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (ExploreDto.PlayerController.IsGrounded) newMainCharacterState = Factory.Idle();

            return newMainCharacterState != null;
        }
    }
}