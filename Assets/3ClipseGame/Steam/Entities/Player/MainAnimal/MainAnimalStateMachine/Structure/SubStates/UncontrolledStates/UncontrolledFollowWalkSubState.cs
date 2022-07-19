using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledFollowWalkSubState : AnimalSubState
    {
        public UncontrolledFollowWalkSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        public override void OnStateEnter(){}
        public override void OnStateUpdate(){}
        public override void OnStateExit(){}
        
        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;
            
            //conditions

            return newAnimalState != null;
        }
    }
}