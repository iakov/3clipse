using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public class Gravity : MonoBehaviour
    {
        #region Initialization

        [Range(0, -10)] [SerializeField] private float gravity = -2f;
        [Range(0, -100)] [SerializeField] private float gravityLimit = -20f;

        private PlayerMover _playerMover;
        private CharacterController _playerController;

        private float _ungroundedTimer;

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _playerMover = GetComponent<PlayerMover>();
            _playerController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_playerController.isGrounded) _ungroundedTimer = 1f;
            else _ungroundedTimer += Time.deltaTime;

            var gravitySquare = -Mathf.Pow(_ungroundedTimer * gravity, 2);
            gravitySquare = gravitySquare < gravityLimit ? gravityLimit : gravitySquare;
            _playerMover.ChangeMove(MoveType.GravityMove, new Vector3(0f, gravitySquare, 0f), RotationType.NoRotation);
        }

        #endregion

        #region PublicMethods

        public void RestartGravity() => _ungroundedTimer = 1f;

        #endregion
    }
}
