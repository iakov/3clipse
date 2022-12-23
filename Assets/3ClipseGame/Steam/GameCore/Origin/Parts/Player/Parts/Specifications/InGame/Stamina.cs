using System;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Parts.Specifications.InGame
{
    public class Stamina : MonoBehaviour
    {
        [SerializeField] private float _maximumStaminaAmount = 100f;
        [SerializeField] private float _staminaRecovery = 7f;

        public event Action StaminaChanged;
        public float StaminaPercentage { get; private set; }
        public bool IsRecovering { get; set; } = true;

        private void Awake()
        {
            StaminaPercentage = 1f;
        }

        private void Update()
        {
            if (IsRecovering) AddValue(_staminaRecovery * Time.deltaTime);
        }

        public void AddValue(float staminaChange)
        {
            if (Math.Abs(StaminaPercentage - 1f) < Mathf.Epsilon && staminaChange > Mathf.Epsilon) return;
            if (StaminaPercentage == 0f && staminaChange < 0f) return;
            EditValue(staminaChange);
        }

        private void EditValue(float value)
        {
            var currentStamina = StaminaPercentage * _maximumStaminaAmount;
            currentStamina += value;

            StaminaPercentage = currentStamina / _maximumStaminaAmount;
            StaminaChanged?.Invoke();

            if (StaminaPercentage > 1f) StaminaPercentage = 1f;
            else if (StaminaPercentage < 0f) StaminaPercentage = 0f;
        }
    }
}
