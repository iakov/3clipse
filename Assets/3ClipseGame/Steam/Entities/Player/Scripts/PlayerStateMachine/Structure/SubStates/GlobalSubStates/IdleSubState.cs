using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.GlobalSubStates
{
    public class IdleSubState : SubState
    {
        public IdleSubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}

        public override void OnStateEnter(){}

        public override void OnStateUpdate(){}

        public override void OnStateExit(){}

        public override bool TrySwitchState(out State newState)
        {
            throw new System.NotImplementedException();
        }
    }
}
