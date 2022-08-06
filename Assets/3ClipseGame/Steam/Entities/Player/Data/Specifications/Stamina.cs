using System;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Specifications
{
    public class Stamina : MonoBehaviour
    {
        #region SerializeFields

        public float staminaMax = 100f;
        public float staminaRecovery = 7f;

        #endregion
        
        #region PublicFields

        public float StaminaValue { get; private set; }
        public Action StaminaChanged;
        public bool IsRecovering { get; set; } = true;
        

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => StaminaValue = staminaMax;

        private void Update()
        {
            if (IsRecovering) AddValue(staminaRecovery * Time.deltaTime);
        }
        

        #endregion
        
        #region PublicMethods

        public void AddValue(float staminaChange)
        {
            var previousStamina = StaminaValue;
            StaminaValue += staminaChange;
            
            if (StaminaValue < 0) StaminaValue = 0;
            else if (StaminaValue > 100) StaminaValue = 100;

            if (Math.Abs(previousStamina - StaminaValue) > 0) StaminaChanged?.Invoke();
        }

        #endregion
    }
}
