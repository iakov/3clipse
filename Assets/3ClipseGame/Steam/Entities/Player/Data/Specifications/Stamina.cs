using System;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Specifications
{
    public class Stamina : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private float _maximumStaminaAmount = 100f;
        [SerializeField] private float _staminaRecovery = 7f;

        #endregion
        
        #region PublicFields

        public Action StaminaChanged;
        public float StaminaPercentage { get; private set; }
        public bool IsRecovering { get; set; } = true;


        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            StaminaPercentage = 1f;
        }

        private void Update()
        {
            if (IsRecovering) AddValue(_staminaRecovery * Time.deltaTime);
        }
        

        #endregion
        
        #region PublicMethods

        public void AddValue(float staminaChange)
        {
            if (Math.Abs(StaminaPercentage - 1f) < Mathf.Epsilon && staminaChange > Mathf.Epsilon) return;
            if (StaminaPercentage == 0f && staminaChange < 0f) return;

            var currentStamina = StaminaPercentage * _maximumStaminaAmount;
            currentStamina += staminaChange;

            StaminaPercentage = currentStamina / _maximumStaminaAmount;
            StaminaChanged?.Invoke();

            if (StaminaPercentage > 1f) StaminaPercentage = 1f;
            else if (StaminaPercentage < 0f) StaminaPercentage = 0f;
        }

        #endregion
    }
}
