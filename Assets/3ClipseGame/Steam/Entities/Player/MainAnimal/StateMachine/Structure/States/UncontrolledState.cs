using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public class UncontrolledState : AnimalState
    {
        #region Initialization

        public UncontrolledState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        private UncontrolledSubStatesFactory _subStateFactory;
        private Vector3 _currentTarget;

        #endregion

        #region StateMethods

        public override void OnStateEnter()
        {
            _subStateFactory = new UncontrolledSubStatesFactory(Context);
            CurrentSubState = _subStateFactory.Idle();
            
            base.OnStateEnter();

            Context.AnimalAgent.enabled = true;
            Context.AnimalController.enabled = false;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            
            Context.AnimalAgent.enabled = false;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.IsSwitching) newAnimalState = Factory.ControlledState();

            return newAnimalState != null;
        }

        #endregion
    }
}
