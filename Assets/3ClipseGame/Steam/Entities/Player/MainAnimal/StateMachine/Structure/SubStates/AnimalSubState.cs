using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates
{
    public abstract class AnimalSubState<T1> : State<AnimalSubState<T1>, T1, MainAnimalStateMachine> where T1 : AnimalSubStateFactory
    {
        protected AnimalSubState(MainAnimalStateMachine context, T1 factory) : base(context, factory){}

        public abstract override void OnStateEnter();

        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out AnimalSubState<T1> newSubState);
    }
}
