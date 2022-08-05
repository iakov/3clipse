using System;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Scripts
{
    public class DisableOnGround : MonoBehaviour
    {
        #region Events

        public Action LootDeactivated;

        #endregion

        #region SerializeFields

        [SerializeField] private float disableTime = 0.5f;

        #endregion

        #region PrivateFields

        private Rigidbody _rigidbody;
        private bool _isEnabled = true;
        private float _staticTimer;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }


        private void Update()
        {
            if (_rigidbody.velocity.magnitude == 0f && _isEnabled) _staticTimer += Time.deltaTime;
            else _staticTimer = 0f;

            if (!(_staticTimer > disableTime) || !_isEnabled) return;
            
            _rigidbody.isKinematic = true;
            _isEnabled = false;
            LootDeactivated?.Invoke();
        }
        
        #endregion
    }
}
