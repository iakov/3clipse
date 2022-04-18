using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    [RequireComponent(typeof(PlayerStateMachine.PlayerStateMachine))]
    [RequireComponent(typeof(Gravity))]
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerRotator))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private PlayerRotator _playerRotator;
        private PlayerStateMachine.PlayerStateMachine _playerStateMachine;

        private void Awake()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine.PlayerStateMachine>();
            _playerMover = GetComponent<PlayerMover>();
            _playerRotator = GetComponent<PlayerRotator>();
        }

        private void Update()
        {
            _playerStateMachine.UpdateWork();
            _playerMover.UpdateWork();
            _playerRotator.UpdateWork();
        }
    }
}
