using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore.SubStates
{
    public class ExploreCrouchSubState : MainCharacterExploreSubState
    {
        public ExploreCrouchSubState(ExploreDto exploreDto, ExploreSubStateFactory factory) : base(exploreDto, factory) {}

        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            var rawInput = new Vector3(ExploreDto.InputProcessor.GetCurrentInput().x, 0f, ExploreDto.InputProcessor.GetCurrentInput().y);
            var moveVector =  ExploreDto.WalkSpeed * ExploreDto.CrouchSpeedModifier * rawInput;
            ExploreDto.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateWithCamera);
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out MainCharacterExploreSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (!ExploreDto.PlayerController.IsGrounded && !Physics.Raycast(ExploreDto.Transform.position, Vector3.down,
                    ExploreDto.PlayerController.Radius)) newMainCharacterState = Factory.Fall();
            else if (!ExploreDto.InputProcessor.GetIsCrouchPressed()) newMainCharacterState = Factory.Idle();
            
            return newMainCharacterState != null;
        }
    }
}