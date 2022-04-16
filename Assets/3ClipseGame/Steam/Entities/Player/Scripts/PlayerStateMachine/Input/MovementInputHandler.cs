using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Input
{
    public class MovementInputHandler : MonoBehaviour
    {
        [NonSerialized] public Vector2 CurrentInput;
        [NonSerialized] public Vector2 LastInput;

        private MovementInput _movementInput;

        private void OnEnable()
        {
            _movementInput = new MovementInput();
            _movementInput.Enable();
            _movementInput.ExploreStateActionMap.Enable();

            _movementInput.ExploreStateActionMap.Walk.started += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.performed += OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.canceled += OnWalkChanged;
        }

        private void OnDisable()
        {
            _movementInput.ExploreStateActionMap.Disable();
            
            _movementInput.ExploreStateActionMap.Walk.started -= OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.performed -= OnWalkChanged;
            _movementInput.ExploreStateActionMap.Walk.canceled -= OnWalkChanged;
        }

        private void OnWalkChanged(InputAction.CallbackContext context) => AddInputLog(context.ReadValue<Vector2>());

        private void AddInputLog(Vector2 newInput)
        {
            Debug.Log("Input Catched");
            LastInput = CurrentInput;
            CurrentInput = newInput;
        }
    }
}
