using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledIdleSubState : AnimalSubState
    {
        public UncontrolledIdleSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}
        public override void OnStateEnter(){}

        public override void OnStateUpdate(){}

        public override void OnStateExit(){}  //TODO: Make MainCharacterStateMachine control all by itself (switch booleans in animator for example)

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            return newAnimalState != null;
        }
    }
}