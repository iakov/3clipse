using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore
{
    public class ExploreMainCharacterState : MainCharacterState
    {
        public ExploreMainCharacterState(MainCharacterStateMachine context, MainCharacterStateFactory factory) : base(context, factory){}
        
        private ExploreSubStatesFactory _subStateFactory;
        private MainCharacterSubState _currentMainCharacterSubState;
        private int _framesFromSwitch;

        public override void OnStateEnter()
        {
            _subStateFactory = new ExploreSubStatesFactory(Context);
            _currentMainCharacterSubState = _subStateFactory.Idle();
            _currentMainCharacterSubState.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _framesFromSwitch++;
            
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

            if (Context.InputProcessor.GetIsSwitched() && _framesFromSwitch >= 2) newMainCharacterState = Factory.AnimalControlState();
            
            return newMainCharacterState != null;
        }

        private void SwitchSubState(MainCharacterSubState nextMainCharacterSubState)
        {
            _currentMainCharacterSubState.OnStateExit();
            _currentMainCharacterSubState = nextMainCharacterSubState;
            _currentMainCharacterSubState.OnStateEnter();
        }
    }
}
