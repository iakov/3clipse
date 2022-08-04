using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class DisableOnGround : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private float disableTime = 0.5f;

        #endregion

        #region PrivateFields

        private Rigidbody _rigidbody;
        private SphereCollider _collider;
        private bool _isEnabled => !_collider.isTrigger;
        private float _staticTimer;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<SphereCollider>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude == 0f && _isEnabled) _staticTimer += Time.fixedDeltaTime;
            else _staticTimer = 0f;

            var isGrounded = Physics.Raycast(_collider.center + _collider.transform.position, Vector3.down,
                _collider.radius + 0.001f);

            if(_staticTimer > disableTime && _isEnabled) _rigidbody.isKinematic = true;
            if(!isGrounded && !_isEnabled) _rigidbody.isKinematic = false;
        }

        #endregion
    }
}
