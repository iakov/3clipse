using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.Visuals.Scripts.AnimationsControllers;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    [RequireComponent(typeof(global::_3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.PlayerStateMachine))]
    [RequireComponent(typeof(Gravity))]
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerAnimationsController))]
    public class Player : MonoBehaviour
    {
        #region PrivateFields

        private PlayerMover _playerMover;
        private PlayerStateMachine.PlayerStateMachine _playerStateMachine;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine.PlayerStateMachine>();
            _playerMover = GetComponent<PlayerMover>();
        }

        private void Update()
        {
            _playerStateMachine.UpdateWork();
            _playerMover.UpdateWork();
        }

        #endregion
    }
}
