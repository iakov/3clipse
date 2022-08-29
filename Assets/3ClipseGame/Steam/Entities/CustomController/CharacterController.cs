using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.CustomController
{
	[RequireComponent(typeof(Rigidbody))]
	public class CharacterController : MonoBehaviour
	{
		#region SerializeFields

		[Header("Global Parameters")] 
		public LayerMask walkableLayers;
		[SerializeField] [Min(0.01f)] private float skinWidth = 0.01f;

		[Header("Move Parameters")] 
		[SerializeField] private float stepOffset = 0.3f;
		[SerializeField] private float groundDetectionDistance = 0.1f;
		[SerializeField] private float minMoveDistance = 0.001f;
		
		[Header("Rotate Parameters")]
		[SerializeField] private RotationInterpolation rotationInterpolationType;
		[SerializeField] [Min(1)] private int interpolationSteps = 5;
		
		[Header("Slope Parameters")]
		[SerializeField] private float slopeLimit = 35f;

		#endregion

		#region PublicFields

		public Vector3 Center => _capsuleCollider == null
			? Vector3.negativeInfinity
			: _capsuleCollider.center + _transform.position;
		public bool IsGrounded { get; private set; }
		public float Radius => _capsuleCollider == null ? -1f : _capsuleCollider.radius;
		public float Height => _capsuleCollider == null ? -1f : _capsuleCollider.height;
		public Vector3 Velocity { get; set; }
		public float DeltaRotation { get; private set; }
		public float DeltaRotationRaw { get; private set; }
		public float CurrentSlope { get; private set; }

		#endregion

		#region PrivateFields

		private float _slideTimer;
		
		private Vector3 _position;

		private Rigidbody _rigidbody;
		private CapsuleCollider _capsuleCollider;
		private Transform _transform;

		private readonly List<RaycastHit> _contacts = new();
		private readonly Collider[] _overlaps = new Collider[5];

		private Quaternion _currentRotateTarget;

		#endregion
		
		#region Initialization

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_capsuleCollider = GetComponentInChildren<CapsuleCollider>();
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

		#region PublicMethods

		public void Move(Vector3 motion)
		{
			PrepareForMove();
			HandleSlope();
			ProceedMove(motion);
			DePenetrate();
			ApplyChanges();
		}

		public void Rotate(Quaternion rotation)
		{
			var oldRotation = _capsuleCollider.transform.rotation;
			ProceedRotation(rotation, oldRotation);
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
			var lateralVelocity = new Vector3(move.x, 0, move.z);
			Sweep(lateralVelocity.normalized, lateralVelocity.magnitude, stepOffset, 145);
			
			var verticalVelocity = new Vector3(0, move.y, 0);
			Sweep(verticalVelocity.normalized, verticalVelocity.magnitude, 0);
		}

		private void Sweep(Vector3 direction, float distance, float verticalOffset, float minSlideAngle = 0, float maxSlideAngle = 360)
		{
			var capsuleOffset = Height / 2 - Radius;

			for (var i = 0; i < 5 && distance > minMoveDistance; i++)
			{
				var origin = _position + _capsuleCollider.center - direction * Radius;
				var bottom = origin - Vector3.up * (capsuleOffset - verticalOffset);
				var top = origin + Vector3.up * capsuleOffset;

				if (Physics.CapsuleCast(top, bottom, Radius, direction, out var hitInfo, distance + Radius, walkableLayers, QueryTriggerInteraction.Ignore))
				{
					var slideAngle = Vector3.Angle(Vector3.up, hitInfo.normal);
					var safeDistance = hitInfo.distance - Radius - skinWidth;
					_position += direction * safeDistance;
					_contacts.Add(hitInfo);

					 if (slideAngle >= minSlideAngle && slideAngle <= maxSlideAngle) break;

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
		
		private void DePenetrate()
		{
			var capsuleOffset = Height / 2 - Radius;
			var top = _position + Vector3.up * capsuleOffset;
			var bottom = _position + Vector3.down * capsuleOffset;
			var overlapsNum = Physics.OverlapCapsuleNonAlloc(top, bottom, Radius, _overlaps, walkableLayers, QueryTriggerInteraction.Ignore);

			if (overlapsNum <= 0) return;
			
			for (var i = 0; i < overlapsNum; i++)
			{
				if (_overlaps[i].transform == _transform || !Physics.ComputePenetration(
					    _capsuleCollider, _position, _transform.rotation,
					    _overlaps[i], _overlaps[i].transform.position, _overlaps[i].transform.rotation,
					    out var direction, out var distance)) continue;
				
				_position += direction * (distance + skinWidth);
			}
		}

		private void HandleSlope()
		{
			if (!IsGrounded || !Physics.SphereCast(Center, Radius, Vector3.down, out var hit)) return;

			CurrentSlope = Vector3.Angle(Vector3.up, hit.normal);
			
		}

		private void ApplyChanges(){
			Velocity = _position - _transform.position;
		}

		#endregion

		#region PrivateRotateMethods

		private void ProceedRotation(Quaternion newRotation, Quaternion oldRotation)
		{
			if (newRotation == _currentRotateTarget) return;
			_currentRotateTarget = newRotation;
			
			DeltaRotationRaw = Quaternion.Angle(newRotation, oldRotation);
			if (newRotation.y - oldRotation.y < 0) DeltaRotationRaw *= -1;

			switch (rotationInterpolationType)
			{
				case RotationInterpolation.None:
					RotateNoInterpolation(newRotation);
					break;
				case RotationInterpolation.Linear:
					StopAllCoroutines();
					StartCoroutine(RotateLinear(newRotation));
					break;
				case RotationInterpolation.Spherical:
					StopAllCoroutines();
					StartCoroutine(RotateSpherical(newRotation));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private IEnumerator RotateLinear(Quaternion rotation)
		{
			var oldRotation = _capsuleCollider.transform.rotation;
			var stepNumber = 1;
			
			while (stepNumber <= interpolationSteps)
			{
				_capsuleCollider.transform.rotation = Quaternion.Lerp(oldRotation, rotation, (float) stepNumber/interpolationSteps);
				DeltaRotation = Quaternion.Angle(_transform.rotation, oldRotation);
				if (_capsuleCollider.transform.rotation.y - oldRotation.y < 0) DeltaRotation *= -1;

				stepNumber++;
				oldRotation = _capsuleCollider.transform.rotation;
				yield return null;
			}
		}
		private IEnumerator RotateSpherical(Quaternion rotation)
		{
			var oldRotation = _capsuleCollider.transform.rotation;
			var stepNumber = 1;
			
			while (stepNumber <= interpolationSteps)
			{
				_capsuleCollider.transform.rotation = Quaternion.Slerp(oldRotation, rotation, (float) stepNumber/interpolationSteps);
				DeltaRotation = Quaternion.Angle(_transform.rotation, oldRotation);
				if (_transform.rotation.y - oldRotation.y < 0) DeltaRotation *= -1;

				stepNumber++;
				oldRotation = _capsuleCollider.transform.rotation;
				yield return null;
			}
		}

		private void RotateNoInterpolation(Quaternion rotation)
		{
			_capsuleCollider.transform.rotation = rotation;
		}

		#endregion

		#region PrivateStructs

		private enum RotationInterpolation
		{
			Linear, Spherical, None
		}

		#endregion
	}
}
