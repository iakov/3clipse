using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledSubStatesFactory : AnimalSubStateFactory
    {
        public UncontrolledSubStatesFactory(StateMachine.MainAnimalStateMachine context) : base(context){}

        public AnimalSubState Idle() => new UncontrolledIdleSubState(Context, this);
        public AnimalSubState Walk() => new UncontrolledFollowWalkSubState(Context, this);
        public AnimalSubState Run() => new UncontrolledFollowRunSubState(Context, this);
        public AnimalSubState Entertain() => new UncontrolledEntertainSubState(Context, this);
        public AnimalSubState WalkBack() => new UncontrolledWalkBackSubState(Context, this);

    }
}