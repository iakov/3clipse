using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledFallSubState : ControlledSubState
    {
        #region Initialization

        public ControlledFallSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory) {}

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.Stamina.IsRecovering = false;
        }

        public override void OnStateUpdate()
        {
            StateTimer += Time.deltaTime;
        }

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (Context.AnimalController.IsGrounded) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }
        
        #endregion
    }
}