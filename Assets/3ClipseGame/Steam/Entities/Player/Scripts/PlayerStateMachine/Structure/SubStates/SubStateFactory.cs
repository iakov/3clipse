using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates
{
    public abstract class SubStateFactory : StateFactory
    {
        public SubStateFactory(PlayerStateMachine context) : base(context){}
    }
}

