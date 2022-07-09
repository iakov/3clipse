using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public class Gravity : MonoBehaviour
    {
        #region Initialization

        [Range(0, -10)] [SerializeField] private float gravity = -9.8f;
        [Range(0, -100)] [SerializeField] private float gravityLimit = -30f;

        private PlayerMover _playerMover;
        private MainCharacter.MainCharacter _mainCharacter;

        private float _ungroundedTimer;

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _playerMover = GetComponent<PlayerMover>();
            _mainCharacter = GetComponent<MainCharacter.MainCharacter>();
        }

        private void Update()
        {
            if (_mainCharacter.IsGrounded) _ungroundedTimer = 0f;
            else _ungroundedTimer += Time.deltaTime;

            var fallSpeed = gravity * _ungroundedTimer;
            fallSpeed = fallSpeed < gravityLimit ? gravityLimit : fallSpeed;
            _playerMover.ChangeMove(MoveType.GravityMove, new Vector3(0f, fallSpeed, 0f), RotationType.NoRotation);
        }

        #endregion

        #region PublicMethods

        public void RestartGravity() => _ungroundedTimer = 1f;

        #endregion
    }
}
