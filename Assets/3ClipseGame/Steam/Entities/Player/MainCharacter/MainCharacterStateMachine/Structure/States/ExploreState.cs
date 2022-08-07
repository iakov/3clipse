using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using _3ClipseGame.Steam.Global.Scripts.GameScripts;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States
{
    public class ExploreMainCharacterState : MainCharacterState
    {
        #region Initialization

        public ExploreMainCharacterState(MainCharacterStateMachine context, MainCharacterStateFactory factory) : base(context, factory){}
        
        private ExploreSubStatesFactory _subStateFactory;
        private MainCharacterSubState _currentMainCharacterSubState;

        #endregion

        #region StateMethods

        public override void OnStateEnter()
        {
            Game.Instance.StateDrivenCamera.SwitchCamera(CameraAnimatorController.CameraType.MainCharacter);
            
            _subStateFactory = new ExploreSubStatesFactory(Context);
            _currentMainCharacterSubState = _subStateFactory.Idle();
            _currentMainCharacterSubState.OnStateEnter();
            Context.InputHandler.SwitchToExploreControls();
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            if (_currentMainCharacterSubState.TrySwitchState(out var nextSubState)) SwitchSubState((MainCharacterSubState)nextSubState);
            _currentMainCharacterSubState.OnStateUpdate();
        }

        public override void OnStateExit()
        {
            Context.PlayerMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.NoRotation);
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (Context.InputHandler.IsSwitchPressed) newMainCharacterState = Factory.AnimalControlState();
            
            return newMainCharacterState != null;
        }

        #endregion

        #region PrivateMethods

        private void SwitchSubState(MainCharacterSubState nextMainCharacterSubState)
        {
            base.SwitchSubState(_currentMainCharacterSubState, nextMainCharacterSubState);
            _currentMainCharacterSubState.OnStateExit();
            _currentMainCharacterSubState = nextMainCharacterSubState;
            _currentMainCharacterSubState.OnStateEnter();
        }

        #endregion
    }
}
