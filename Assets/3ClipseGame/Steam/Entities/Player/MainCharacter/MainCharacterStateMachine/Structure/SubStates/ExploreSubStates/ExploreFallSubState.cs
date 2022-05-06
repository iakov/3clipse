using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreFallSubState : MainCharacterSubState
    {
        #region Initialization
        public ExploreFallSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;
        
        private ExploreSubStatesFactory _factory;
        
        #endregion

        #region SubStateMethods

        public override void OnStateEnter(){}

        public override void OnStateUpdate(){}

        public override void OnStateExit(){}

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            var legsRay = new Ray(Context.MainCharacterAnimator.GetBoneTransform(HumanBodyBones.LeftFoot).position, Vector3.down);
            if (Physics.Raycast(legsRay, 0.5f, Context.WalkableLayerMask)) newMainCharacterState = _factory.Idle();

            return newMainCharacterState != null;
        }

        #endregion
    }
}