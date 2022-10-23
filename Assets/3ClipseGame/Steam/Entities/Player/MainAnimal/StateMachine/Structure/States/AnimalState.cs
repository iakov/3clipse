using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public abstract class AnimalState : State<AnimalState, AnimalStateFactory, MainAnimalStateMachine>
    {
        protected AnimalState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        public abstract override void OnStateEnter();

        public abstract override void OnStateExit();
    }
}
