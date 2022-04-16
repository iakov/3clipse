using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    [RequireComponent(typeof(PlayerStateMachine.PlayerStateMachine))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private PlayerStateMachine.PlayerStateMachine _playerStateMachine;

        private void Awake()
        {
            _playerStateMachine = GetComponent<PlayerStateMachine.PlayerStateMachine>();
        }
        
        private void Start()
        {
            _playerStateMachine.StartWork();
            _playerMover = _playerStateMachine.PlayerMover;
        }

        private void Update()
        {
            _playerStateMachine.UpdateWork();
            _playerMover.UpdateWork();
        }
    }
}
