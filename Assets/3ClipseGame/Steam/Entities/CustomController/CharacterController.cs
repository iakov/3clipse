using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.CustomController
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class CharacterController : MonoBehaviour
	{
		#region SerializeFields

		[Header("Global Parameters")] 
		public LayerMask walkableLayers;
		[SerializeField] [Min(0.01f)] private float skinWidth = 0.01f;

		[Header("Move Parameters")] 
		[SerializeField] private float stepOffset = 0.3f;
		[SerializeField] private float groundDetectionDistance = 0.1f;
		
		[Header("Slope Parameters")]
		[SerializeField] [Min(0f)] private float slopeSlideSpeed = 2f;
		[SerializeField] private float slopeLimit = 35f;
		[SerializeField] private AnimationCurve slideSpeedUpModifier;

		#endregion

		#region PublicFields

		public Vector3 Center => _capsuleCollider == null
			? Vector3.negativeInfinity
			: _capsuleCollider.center + _transform.position;

		public bool IsGrounded { get; private set; }
		public float Radius => _capsuleCollider == null ? -1f : _capsuleCollider.radius;
		public float Height => _capsuleCollider == null ? -1f : _capsuleCollider.height;
		public Vector3 Velocity { get; set; }
		public float CurrentSlope { get; private set; }

		#endregion

		#region PrivateFields

		private float _slideTimer;
		
		private Vector3 _position;

		private Rigidbody _rigidbody;
		private CapsuleCollider _capsuleCollider;
		private Transform _transform;

		private readonly List<RaycastHit> _contacts = new();

		#endregion
		
		#region Initialization

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_capsuleCollider = GetComponent<CapsuleCollider>();
			_transform = GetComponent<Transform>();

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

		private void FixedUpdate()
		{
			SetIsGrounded();
			if(Velocity != Vector3.zero) _rigidbody.MovePosition(_position);
		}

		private void SetIsGrounded()
		{
			IsGrounded = Physics.SphereCast(Center, Radius, Vector3.down, out _,
				Height / 2 - Radius + groundDetectionDistance, walkableLayers);
		}

		#endregion

		#region PublicMoveMethods

		public void Move(Vector3 motion)
		{
			PrepareForMove();
			ProceedMove(motion);
			HandleSlopeSlide();
			ApplyChanges();
		}

		#endregion

		#region PrivateMoveMethods

		private void PrepareForMove()
		{
			_position = _rigidbody.position;
			Velocity = Vector3.zero;
			_contacts.Clear();
		}

		private void ProceedMove(Vector3 move)
		{
			//Handle vertical movement
			var verticalMove = new Vector3(0f, move.y, 0f);
			var capsuleOffset = Height / 2 - Radius;
			if (Physics.SphereCast(Center, Radius + skinWidth, verticalMove, out var hitInfo,
				    verticalMove.magnitude + capsuleOffset))
			{
				_position += verticalMove.normalized * (hitInfo.distance - capsuleOffset);
				_contacts.Add(hitInfo);
			}
			else
			{
				_position += verticalMove;
			}

			//Handle horizontal movement
			var horizontalMove = new Vector3(move.x, 0f, move.z);
			var direction = horizontalMove.normalized;
			var distance = horizontalMove.magnitude;
			
			for (var i = 0; i < 5; i++)
			{
				var origin = _position + _capsuleCollider.center - direction * Radius;
				var top = origin + Vector3.up * capsuleOffset;
				var bottom = origin + Vector3.down * (capsuleOffset - stepOffset);

				var isWalled = Physics.CapsuleCast(bottom, top, Radius, direction, out hitInfo, distance + Radius);
				
				if (isWalled && !hitInfo.collider.isTrigger)
				{
					var safeDistance = hitInfo.distance - Radius - skinWidth;
					_position += direction * safeDistance;
					_contacts.Add(hitInfo);
					
					direction = Vector3.ProjectOnPlane(direction, hitInfo.normal);
					distance -= safeDistance;
				}
				
				else
				{
					_position += direction * distance;
					break;
				}
			}
		}

		private void HandleSlopeSlide()
		{
			if (!IsGrounded || !Physics.Raycast(Center, Vector3.down, out var hit)) return;

			var angle = Vector3.Angle(Vector3.up, hit.normal);
			var slideMove = new Vector3(hit.normal.x, -hit.normal.y, hit.normal.z);
			
			if (angle >= slopeLimit)
			{
				_slideTimer += Time.fixedDeltaTime;
				var maxSlideTime = slideSpeedUpModifier.keys[slideSpeedUpModifier.length - 1].time;
				if (_slideTimer > maxSlideTime) _slideTimer = maxSlideTime;
				var slideModifier = slideSpeedUpModifier.Evaluate(_slideTimer);
				
				_position += slideMove * (Time.fixedDeltaTime * slopeSlideSpeed * slideModifier);
			}
			else
			{
				_slideTimer = 0f;
			}
		}

		private void ApplyChanges(){
			Velocity = _position - _transform.position;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			foreach (var contact in _contacts)
			{
				Gizmos.DrawWireSphere(contact.point, 0.05f);
			}
		}

		#endregion
	}
}
