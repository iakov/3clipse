using System;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.HUDInput;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class InteractablesInputProcessor : InputProcessor
    {
        [SerializeField] private HUDInputHandler _hudInputHandler;
        
        public event Action Interacted;
        public event Action ScrolledForward;
        public event Action ScrolledBack;

        private void OnLootActivated() => Interacted?.Invoke();
        private void OnPreviousInteractableSelected() => ScrolledBack?.Invoke();
        private void OnNextInteractableSelected() => ScrolledForward?.Invoke();

        private void Awake()
        {
            _hudInputHandler.Interacted += OnLootActivated;
            _hudInputHandler.ScrollForward += OnNextInteractableSelected;
            _hudInputHandler.ScrollBack += OnPreviousInteractableSelected;
        }
        
        public override void Enable()
        {
            _hudInputHandler.Enable();
        }

        public override void Disable()
        {
            _hudInputHandler.Disable();
        }
    }
}