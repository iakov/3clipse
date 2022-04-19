using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    [RequireComponent(typeof(PlayerStateMachine.PlayerStateMachine))]
    [RequireComponent(typeof(Gravity))]
    [RequireComponent(typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private PlayerStateMachine.PlayerStateMachine _playerStateMachine;

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
    }
}
