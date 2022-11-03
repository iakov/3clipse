using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreSlideSubState : MainCharacterExploreSubState
    {
        public ExploreSlideSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        public override void OnStateEnter()
        {
            ExploreDto.SaveLastMove(true);
            SwitchStamina(false);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            ChangeMove();
        }

        private void ChangeMove()
        {
            var speedModifier = ExploreDto.SlideModifierCurve.Evaluate(StateTimer);
            var slideMoveVector = ExploreDto.LastMove * speedModifier;
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, slideMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
            SwitchStamina(true);
        }

        private void SwitchStamina(bool isRecovering)
        {
            var stamina = ExploreDto.Stamina;
            stamina.IsRecovering = isRecovering;
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (IsStopped()) newMainCharacterState = Factory.Crouch();
            else if (IsCrouched()) newMainCharacterState = Factory.Jump();
            
            return newMainCharacterState != null;
        }

        private bool IsStopped()
        {
            var currentTime = StateTimer;
            var slideCurve = ExploreDto.SlideModifierCurve;
            var maxSlideTime = slideCurve.keys[slideCurve.length - 1].time;

            return currentTime >= maxSlideTime;
        }

        private bool IsCrouched()
        {
            return ExploreDto.InputProcessor.GetIsCrouchPressed();
        }
    }
}