using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public class Gravity : MonoBehaviour
    {
        [SerializeField] private float gravity = -1f;
        [SerializeField] private float gravityLimit = -20f;

        private PlayerMover _playerMover;
        private CharacterController _playerController;

        private float _ungroundedTimer;

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
            _playerMover.ChangeMove(MoveType.GravityMove, new Vector3(0f, gravitySquare, 0f));
        }
    }
}
