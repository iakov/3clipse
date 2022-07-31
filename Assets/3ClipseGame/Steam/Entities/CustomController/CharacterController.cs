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

		public bool IsGrounded => Physics.SphereCast(Center, Radius, Vector3.down, out _,
			Height / 2 - Radius + groundDetectionDistance, walkableLayers);
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

		private Vector3 _capsuleBottomPoint => new(Center.x, Center.y - Height / 2 + Radius - skinWidth, Center.z);
		private Vector3 _capsuleNoStepPoint => new(Center.x, Center.y - Height / 2 + Radius + stepOffset - skinWidth, Center.z);
		private Vector3 _capsuleUpperPoint => new(Center.x, Center.y + Height / 2 - Radius + skinWidth, Center.z);

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

		#region MoveMethods

		public void Move(Vector3 motion)
		{
			SetPreMoveVariables(motion);
			ClearOldData();
			MoveWithCollisions();
			HandleCollisions();
			SetState();
		}

		private void SetPreMoveVariables(Vector3 motion)
		{
			_upDirection = _transform.up;
			Velocity = motion;
			_deltaPosition = Vector3.zero;
		}

		private void SetState()
		{
			var overlaps = new Collider[5];
			Velocity = _deltaPosition;
			_transform.position += _deltaPosition;
			if (Physics.OverlapCapsuleNonAlloc(_capsuleUpperPoint, _capsuleBottomPoint, Radius, overlaps) != 0) DePenetrate();
		}
		private void ClearOldData() => _contacts.Clear();

		private void MoveWithCollisions()
		{
			if (!(Velocity.sqrMagnitude > minMoveDistance)) return;

			var verticalMove = new Vector3(0f, Velocity.y, 0f);
			verticalMove *= Time.deltaTime;

			var lateralVelocity = new Vector3(Velocity.x, 0, Velocity.z);
			lateralVelocity *= Time.deltaTime;

			MoveVertical(verticalMove);
			MoveHorizontal(lateralVelocity);
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
				_deltaPosition.y += travelDistanceToCollision;
				_contacts.Add(overlapHit);
			}
			else
			{
				_deltaPosition += move;
			}
		}

		private void MoveHorizontal(Vector3 move)
		{
			if ((move / Time.deltaTime).magnitude < minMoveDistance) return;

			var top = _capsuleUpperPoint + _deltaPosition;
			var bottom = _capsuleNoStepPoint + _deltaPosition;

			if (Physics.CapsuleCast(top, bottom, Radius + skinWidth, move, out var hitInfo, move.magnitude))
			{
				var slideMove = Vector3.ProjectOnPlane(move, hitInfo.normal) * 0.7f;
				if (Physics.CapsuleCast(top, bottom, Radius + skinWidth, slideMove, out var slideHitInfo,
					    slideMove.magnitude)) _deltaPosition += slideMove * slideHitInfo.distance;
				else _deltaPosition += slideMove;
				
				if(Physics.CheckCapsule(top + _deltaPosition, bottom + _deltaPosition, Radius)) DePenetrate();
			}
			else _deltaPosition += move;
		}

		private void HandleCollisions()
		{
			if (_contacts.Count <= 0) return;

			foreach (var contact in _contacts)
			{
				var angle = Vector3.Angle(_upDirection, contact.normal);

				//if (angle >= slopeLimit) _position -= Vector3.Cross(contact.normal, contact.transform.forward);
			}
		}
		
		private void DePenetrate()
		{
			var overlaps = new Collider[5];
			var overlapsNum = Physics.OverlapCapsuleNonAlloc(_capsuleNoStepPoint, _capsuleUpperPoint, Radius, overlaps);

			if (overlapsNum <= 0) return;
			for (var i = 0; i < overlapsNum; i++)
			{
				if (overlaps[i].transform == _transform || !Physics.ComputePenetration(_capsuleCollider, _transform.position,
					    transform.rotation,
					    overlaps[i], overlaps[i].transform.position, overlaps[i].transform.rotation,
					    out var direction, out var distance)) continue;
				_transform.position += direction * (distance + skinWidth);
			}
		}

		#endregion
	}
}
