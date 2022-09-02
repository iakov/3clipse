using System;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker
{
    public class InactiveLootDisabler : MonoBehaviour
    {
        #region Events

        public Action LootDeactivated;

        #endregion

        #region SerializeFields

        [SerializeField] private float _disableTime = 0.5f;

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

            if (_staticTimer < _disableTime || _isEnabled == false) return;
            
            _rigidbody.isKinematic = true;
            _isEnabled = false;
            LootDeactivated?.Invoke();
        }
        
        #endregion
    }
}
