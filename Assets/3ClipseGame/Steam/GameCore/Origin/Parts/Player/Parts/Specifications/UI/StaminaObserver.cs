using System.Collections;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Parts.Specifications.UI
{
    public class StaminaObserver : MonoBehaviour
    {
        private Slider _staminaSlider;
        private PlayerEntity _currentEntity => GameSource.Instance.GetPlayer().GetCurrentPlayerEntity();

        private void Awake() => _staminaSlider = GetComponent<Slider>();
        
        private void OnEnable()
        {
            _currentEntity.Stamina.StaminaChanged += OnStaminaChanged;
        }

        private void OnDisable()
        {
            _currentEntity.Stamina.StaminaChanged -= OnStaminaChanged;
        }

        private void Start()
        {
            OnStaminaChanged();
        }

        private void OnStaminaChanged()
        {
            StartCoroutine(ChangeSlider());
        }

        private IEnumerator ChangeSlider()
        {
            yield return null;
            _staminaSlider.value = _currentEntity.Stamina.StaminaPercentage;
        }
    }
}