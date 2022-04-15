using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates
{
    public abstract class SubState : State
    {
        protected SubState(PlayerStateMachine context, SubStateFactory factory) : base(context, factory){}
    }
}
