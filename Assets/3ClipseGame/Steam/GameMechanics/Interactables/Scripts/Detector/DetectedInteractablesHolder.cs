using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Scripts.Detector
{
    public class DetectedInteractablesHolder
    {
        public event Action<Interactable> InteractableRemoved;
        public event Action<Interactable> InteractableAdded;
        
        public static DetectedInteractablesHolder Empty() => 
            new DetectedInteractablesHolder();
        
        private DetectedInteractablesHolder() => 
            _detectedLoot = new List<GameObject>();
        
        private readonly List<GameObject> _detectedLoot;

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

        public bool Contains([NotNull] Interactable lootComponent) =>
            _detectedLoot.Contains(lootComponent.gameObject);

        private void AddDetected([NotNull] Interactable interactable)
        {
            _detectedLoot.Add(interactable.gameObject);
            interactable.Disappeared += RemoveDetected;
            InteractableAdded?.Invoke(interactable);
        }

        private void RemoveDetected(Interactable interactable)
        {
            _detectedLoot.Remove(interactable.gameObject);
            interactable.Disappeared -= RemoveDetected;
            InteractableRemoved?.Invoke(interactable);
        }
    }
}