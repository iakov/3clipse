using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.Specifications;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Global.UI.Scripts.Specifications
{
    public class StaminaObserver : MonoBehaviour
    {
        #region PrivateFields
        
        private Slider _staminaSlider;
        [SerializeField] private Stamina stamina;
        
        #endregion

        #region MonoBehaviourMethods
        
        private void Awake() => _staminaSlider = GetComponent<Slider>();
        
        private void OnEnable()
        {
            StartCoroutine(ChangeSlider());
            stamina.StaminaChanged += OnStaminaChanged;
        }
        
        private void OnDisable() => stamina.StaminaChanged -= OnStaminaChanged;

        private void OnStaminaChanged() => StartCoroutine(ChangeSlider());
        
        private IEnumerator ChangeSlider()
        {
            yield return null;
            _staminaSlider.value = stamina.StaminaValue / stamina.staminaMax;
        }

        #endregion
    }
}