using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.CustomController
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class CharacterController : MonoBehaviour
	{
		#region PublicFields
        public Vector3 Center => _capsuleCollider.center + _transform.position;
        public LayerMask WalkableLayers;
        public bool IsGrounded { get; private set; }
        public float Radius => _capsuleCollider.radius;
        public float Height => _capsuleCollider.height;
        public Vector3 Velocity { get; private set; }

        #endregion

        #region PrivateFields
        
        [Header("Vertical Move Parameters")]
        [SerializeField] private float _groundDetectionDistance = 0.1f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _gravityLimit = -30f;

        [Header("Horizontal Move Offset")] 
        [SerializeField] private float _minimumMoveDistance = 0.01f;
        [SerializeField] private float _stepOffset;

        private Rigidbody _rigidbody;
        private CapsuleCollider _capsuleCollider;
        private Transform _transform;
        private PlayerMover _playerMover;

        private float _ungroundedTimer;
        
        private Vector3 _horizontalVelocity;
        private Vector3 _verticalVelocity;
        private Vector3 _moveDirection;
        private Vector3 _verticalHitPoint;
        private Vector3 _horizontalHitPoint;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            _playerMover = GetComponent<PlayerMover>();

            SetRigidbodyParams();
            SetColliderParams();
        }
        
        private void SetRigidbodyParams()
        {
            _rigidbody.isKinematic = true;
        }

        private void SetColliderParams()
        {
            
        }

        private void FixedUpdate()
        {
            SetIsGrounded();
            SetGravity();
        }

        #endregion
        
        #region PhysicsMethods

        private void SetGravity()
        {
            if (IsGrounded) _ungroundedTimer = 0f;
            else _ungroundedTimer += Time.deltaTime;

            if (IsGrounded) _ungroundedTimer = 0.5f;

            var fallSpeed = _gravity * _ungroundedTimer;
            fallSpeed = fallSpeed < _gravityLimit ? _gravityLimit : fallSpeed;
            _playerMover.ChangeMove(MoveType.GravityMove, new Vector3(0f, fallSpeed, 0f), RotationType.NoRotation);
        }

        private void SetIsGrounded()
        {
            IsGrounded = Physics.SphereCast(Center, Radius, Vector3.down, out var _,
                Height / 2 - Radius + _groundDetectionDistance, WalkableLayers);
        }

        #endregion

        #region MoveMethods

        public void Move(Vector3 moveDirection)
        {
            moveDirection *= Time.deltaTime;
            
            var verticalMove = new Vector3(0f, moveDirection.y, 0f);
            var horizontalMove = new Vector3(moveDirection.x, 0f, moveDirection.z);

            MoveHorizontal(horizontalMove);
            MoveVertical(verticalMove);
            
            Velocity = _verticalVelocity + _horizontalVelocity;
            if (Velocity.magnitude < _minimumMoveDistance) Velocity = Vector3.zero;
            
            _transform.position += Velocity;
        }

        private void MoveVertical(Vector3 move)
        {
            var maxDistance = Height / 2 - Radius + Mathf.Abs(move.y);
            var isHitted = Physics.SphereCast(Center, Radius, move.normalized, out var overlapHit, maxDistance);

            if (isHitted)
            {
                var distanceToCollisionSphereCenter = (overlapHit.distance * move.normalized).y;
                var lowestCapsuleSphereCenter = Center.y - Height / 2 + Radius;
                var distanceToLowestCapsuleSphereCenter = lowestCapsuleSphereCenter - Center.y;
                var travelDistanceToCollision = distanceToCollisionSphereCenter - distanceToLowestCapsuleSphereCenter;
                _verticalVelocity = new Vector3(0f, travelDistanceToCollision, 0f);
            }
            else
            {
                _verticalVelocity = move;
            }

            _verticalHitPoint = overlapHit.point;
        }

        private void MoveHorizontal(Vector3 move)
        {
            var point1 = new Vector3(Center.x, Center.y - Height / 2 + Radius, Center.z);
            var point2 = new Vector3(Center.x, Center.y + Height / 2 - Radius, Center.z);
            
            var isHitted = Physics.CapsuleCast(point1, point2, Radius, move.normalized, out var overlapHit, move.magnitude * 2);

            if (isHitted)
            {
                _horizontalVelocity = Vector3.zero;

                _horizontalHitPoint = overlapHit.point;
            }
            else
            {
                _horizontalVelocity = move;
                _horizontalHitPoint = Vector3.zero;
            }
        }

        #endregion

        #region Debugging

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Center, 0.02f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(Center, Center + _horizontalVelocity * 30);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(Center, Center + _verticalVelocity * 5);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_verticalHitPoint, 0.02f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_horizontalHitPoint, 0.02f);
            
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(new Vector3(Center.x, Center.y - Height / 2 + Radius, Center.z), Radius);
            Gizmos.DrawWireSphere(new Vector3(Center.x, Center.y + Height / 2 - Radius, Center.z), Radius);
        }
        
        #endregion
	}
}
