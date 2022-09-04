using System.Data.Common;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates;
using _3ClipseGame.Steam.Global.Scripts.GameScripts;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States
{
    public class ControlledState : AnimalState
    {
        public ControlledState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        private AnimalSubState _currentSubState;
        private ControlledSubStatesFactory _subStateFactory;
        
        public override void OnStateEnter()
        {
            Game.Instance.StateDrivenCamera.SwitchCamera(CameraAnimatorController.CameraType.Animal);
            
            _subStateFactory = new ControlledSubStatesFactory(Context);
            _currentSubState = _subStateFactory.Idle();
            _currentSubState.OnStateEnter();
            
            Context.InputHandler.SwitchToAnimalControls();
            Context.AnimalAgent.enabled = false;
            Context.AnimalController.enabled = true;
        }

        public override void OnStateUpdate()
        {
            if (_currentSubState.TrySwitchState(out var newState)) SwitchState((AnimalSubState) newState);
            _currentSubState.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            _currentSubState.OnStateExit();
            Context.AnimalAgent.enabled = true;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.IsSwitching) newAnimalState = Factory.UncontrolledState();
            
            return newAnimalState != null;
        }
        
        private void SwitchState(AnimalSubState newAnimalSubState)
        {
            SwitchSubState(_currentSubState, newAnimalSubState);
            _currentSubState.OnStateExit();
            _currentSubState = newAnimalSubState;
            _currentSubState.OnStateEnter();
        }
    }
}
