using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.CustomController
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class CharacterController : MonoBehaviour
    {
        #region PublicFields

        public bool IsGrounded { get; private set; }
        public Vector3 Center { get; private set; }
        public float Radius { get; private set; }
        public float Height { get; private set; }

        #endregion

        #region PrivateFields

        [SerializeField] private LayerMask _walkableLayers;
        [SerializeField] private float _groundDetectionDistance = 0.1f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _gravityLimit = -30f;

        private Rigidbody _rigidbody;
        private CapsuleCollider _capsuleCollider;
        private Transform _transform;
        private PlayerMover _playerMover;

        private float _ungroundedTimer;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _playerMover = GetComponent<PlayerMover>();
        }
        
        private void Start(){}

        private void FixedUpdate()
        {
            SetRigidbodyFields();
            SetCapsuleFields();
            SetCenter();
            SetIsGrounded();
            SetGravity();
        }        

        #endregion
        
        #region PrivateMethods

        private void SetRigidbodyFields()
        {
            
        }
        
        private void SetCapsuleFields()
        {
            Radius = _capsuleCollider.radius;
            Height = _capsuleCollider.height;
        }

        private void SetCenter()
        {
            var position = _transform.position;
            Center = new Vector3(position.x, position.y + Height/2, position.z);
        }

        private void SetIsGrounded()
        {
            var legsRay = new Ray(Center, Vector3.down);
            IsGrounded = Physics.SphereCast(legsRay, Radius, Height / 2 - Radius + _groundDetectionDistance, _walkableLayers);
        }

        private void SetGravity()
        {
            if (IsGrounded) _ungroundedTimer = 0f;
            else _ungroundedTimer += Time.deltaTime;

            var fallSpeed = _gravity * _ungroundedTimer;
            fallSpeed = fallSpeed < _gravityLimit ? _gravityLimit : fallSpeed;
            _playerMover.ChangeMove(MoveType.GravityMove, new Vector3(0f, fallSpeed, 0f), RotationType.NoRotation);
        }

        #endregion
    }
}
