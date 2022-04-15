using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.GlobalSubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates
{
    public abstract class SubStateFactory : StateFactory
    {
        public SubStateFactory(PlayerStateMachine context) : base(context){}

        public SubState Idle() => new IdleSubState(base._context, this);
    }
}

