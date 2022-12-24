namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.Play.SubStates
{
    public class AnimalPlayFallSubState : AnimalPlaySubState
    {
        public AnimalPlayFallSubState(AnimalPlayDto dto, AnimalPlaySubStateFactory factory) : base(factory, dto) {}

        public override void OnStateEnter()
        {
            SwitchStaminaRecovery(false);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            SwitchStaminaRecovery(true);
        }

        private void SwitchStaminaRecovery(bool isRecovering)
        {
            var stamina = Dto.Stamina;
            stamina.IsRecovering = isRecovering;
        }

        protected override bool TrySwitch(out AnimalPlaySubState newAnimalState)
        {
            newAnimalState = null;

            if (Dto.AnimalController.IsGrounded) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }
    }
}