using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Detector
{
    public class DetectedInteractablesHolder
    {
        public event Action<Interactable> InteractableRemoved;
        public event Action<Interactable> InteractableAdded;
        
        public static DetectedInteractablesHolder Empty() => 
            new DetectedInteractablesHolder();

        private readonly List<GameObject> _detectedInteractables = new();

        public bool TryAddDetected([NotNull] Interactable interactable)
        {
            if (Contains(interactable)) return false;
            
            AddDetected(interactable);
            return true;
        }
        
        public bool TryRemoveDetected([NotNull] Interactable interactable)
        {
            if (!Contains(interactable)) return false;

            RemoveDetected(interactable);
            return true;
        }

        public bool Contains([NotNull] Interactable interactable) =>
            _detectedInteractables.Contains(interactable.gameObject);

        private void AddDetected([NotNull] Interactable interactable)
        {
            _detectedInteractables.Add(interactable.gameObject);
            InteractableAdded?.Invoke(interactable);
        }

        private void RemoveDetected(Interactable interactable)
        {
            _detectedInteractables.Remove(interactable.gameObject);
            InteractableRemoved?.Invoke(interactable);
        }
    }
}