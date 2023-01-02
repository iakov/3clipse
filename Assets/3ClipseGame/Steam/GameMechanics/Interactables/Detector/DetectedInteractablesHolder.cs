using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Detector
{
    public class DetectedInteractablesHolder
    {
        public event Action<Interactable<InteractablePresenter>> InteractableRemoved;
        public event Action<Interactable<InteractablePresenter>> InteractableAdded;
        
        public static DetectedInteractablesHolder Empty() => 
            new DetectedInteractablesHolder();
        
        private DetectedInteractablesHolder() => 
            _detectedInteractables = new List<GameObject>();
        
        private readonly List<GameObject> _detectedInteractables;

        public bool TryAddDetected([NotNull] Interactable<InteractablePresenter> interactable)
        {
            if (Contains(interactable)) return false;
            
            AddDetected(interactable);
            return true;
        }
        
        public bool TryRemoveDetected([NotNull] Interactable<InteractablePresenter> interactable)
        {
            if (!Contains(interactable)) return false;

            RemoveDetected(interactable);
            return true;
        }

        public bool Contains([NotNull] Interactable<InteractablePresenter> lootComponent) =>
            _detectedInteractables.Contains(lootComponent.gameObject);

        private void AddDetected([NotNull] Interactable<InteractablePresenter> interactable)
        {
            _detectedInteractables.Add(interactable.gameObject);
            interactable.Disappeared += RemoveDetected;
            InteractableAdded?.Invoke(interactable);
        }

        private void RemoveDetected(Interactable<InteractablePresenter> interactable)
        {
            _detectedInteractables.Remove(interactable.gameObject);
            interactable.Disappeared -= RemoveDetected;
            InteractableRemoved?.Invoke(interactable);
        }
    }
}