using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.HUDInput;
using UnityEngine;

namespace _3ClipseGame.Steam.UserInterface.Menu.Scripts.ElementsTweening
{
    public class ElementalWheel : MonoBehaviour
    {
        [SerializeField] private HUDInputHandler _hudInputHandler;
        [SerializeField] private GameObject _wheel;

        private void OnEnable()
        {
            _hudInputHandler.ShowWheelChanged += OnWheelChanged;
        }
        
        private void OnDisable()
        {
            _hudInputHandler.ShowWheelChanged -= OnWheelChanged;
        }

        private void OnWheelChanged(bool isActive)
            => _wheel.SetActive(isActive);
    }
}
