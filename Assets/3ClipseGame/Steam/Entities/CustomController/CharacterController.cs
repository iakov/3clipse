using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.CustomController
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class CharacterController : MonoBehaviour
	{
		#region SerializeFields

		[Header("Global Parameters")] [SerializeField]
		private float slopeLimit = 45f;

		[SerializeField] private float stepOffset = 0.3f;
		[SerializeField] [Min(0.01f)] private float skinWidth = 0.01f;

		public LayerMask walkableLayers;

		[Header("Move Parameters")]
		[SerializeField] private float groundDetectionDistance;
		[SerializeField] [Min(0.01f)] private float minMoveDistance = 0.01f;

		#endregion

		#region PublicFields

		public Vector3 Center => _capsuleCollider == null
			? Vector3.negativeInfinity
			: _capsuleCollider.center + _transform.position;

		public bool IsGrounded { get; private set; }
		public float Radius => _capsuleCollider == null ? -1f : _capsuleCollider.radius;
		public float Height => _capsuleCollider == null ? -1f : _capsuleCollider.height;
		public Vector3 Velocity { get; set; }

		#endregion

		#region PrivateFields

		private float _ungroundedTimer;

		private Vector3 _deltaPosition;
		private Vector3 _upDirection;

		private Rigidbody _rigidbody;
		private CapsuleCollider _capsuleCollider;
		private Transform _transform;
		private PlayerMover _playerMover;

		private readonly List<RaycastHit> _contacts = new();

		private Vector3 _capsuleBottomPoint => new(Center.x, Center.y - Height / 2 + Radius, Center.z);
		private Vector3 _capsuleNoStepPoint => new(Center.x, Center.y - Height / 2 + Radius + stepOffset, Center.z);
		private Vector3 _capsuleUpperPoint => new(Center.x, Center.y + Height / 2 - Radius, Center.z);

		#endregion

		#region Initialization

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
			_rigidbody.useGravity = false;
			_rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			_rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

			_rigidbody.freezeRotation = true;
		}
		
		private void SetColliderParams()
		{
			_capsuleCollider.direction = 1;
			if (_capsuleCollider.height <= _capsuleCollider.radius * 2)
				_capsuleCollider.height = _capsuleCollider.radius * 2 + 0.01f;
		}

		#endregion

		#region PhysicsMethods

		private void FixedUpdate() => SetIsGrounded();

		private void SetIsGrounded()
		{
			IsGrounded = Physics.SphereCast(Center, Radius, Vector3.down, out _,
				Height / 2 - Radius + groundDetectionDistance, walkableLayers);
		}

		#endregion

		#region MoveMethods

		public void Move(Vector3 motion)
		{
			PrepareForMove();
			ProceedMove(motion);
			ApplyChanges();
		}

		private void PrepareForMove()
		{
			_deltaPosition = Vector3.zero;
			Velocity = Vector3.zero;
			_contacts.Clear();
		}

		private void ProceedMove(Vector3 move)
		{
			if (move.magnitude < minMoveDistance) return;
			move *= Time.deltaTime;
			
			//Proceed vertical move
			var verticalMove = new Vector3(0f, move.y, 0f);
			
			if (CheckForCollision(verticalMove, out var hitInfo, false))
			{
				_deltaPosition += hitInfo.distance * verticalMove.normalized - verticalMove;
				_contacts.Add(hitInfo);
			}
			else
			{
				_deltaPosition += verticalMove;
			}
			
			//Proceed horizontal move
			var horizontalMove = new Vector3(move.x, 0f, move.z);

			if (CheckForCollision(horizontalMove, out hitInfo, false))
			{
				
			}
			else
			{
				_deltaPosition += horizontalMove;
			}
		}

		private bool CheckForCollision(Vector3 move, out RaycastHit hitInfo, bool isIgnoreStep)
		{
			var backPointBottom = isIgnoreStep ? _capsuleNoStepPoint - move : _capsuleBottomPoint - move;
			var backPointUpper = _capsuleUpperPoint - move;
			
			return Physics.CapsuleCast(backPointBottom, backPointUpper, Radius, move.normalized,
				out hitInfo, move.magnitude * 2);
		}

		private void ApplyChanges()
		{
			Velocity = _deltaPosition;
			_transform.position += _deltaPosition;
		}

		#endregion
	}
}
