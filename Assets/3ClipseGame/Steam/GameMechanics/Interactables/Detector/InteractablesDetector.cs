using System;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Detector
{
    [RequireComponent(typeof(SphereCollider))]
    
    public class InteractablesDetector : MonoBehaviour
    {
        public event Action<Interactable> InteractableDetected;
        public event Action<Interactable> InteractableRetired;

        private DetectedInteractablesHolder _detectedInteractablesHolder;

        private void Awake()
        {
            _detectedInteractablesHolder = DetectedInteractablesHolder.Empty();
            GetComponent<SphereCollider>().isTrigger = true;
        }

        private void OnEnable()
        {
            _detectedInteractablesHolder.InteractableAdded += OnInteractablesAdded;
            _detectedInteractablesHolder.InteractableRemoved += OnInteractablesRemoved;
        }
        
        private void OnDisable()
        {
            _detectedInteractablesHolder.InteractableAdded -= OnInteractablesAdded;
            _detectedInteractablesHolder.InteractableRemoved -= OnInteractablesRemoved;
        }

        private void OnInteractablesAdded(Interactable interactable) => 
            InteractableDetected?.Invoke(interactable);
        
        private void OnInteractablesRemoved(Interactable interactable) =>
            InteractableRetired?.Invoke(interactable);
        

        private void OnTriggerEnter(Collider other)
        {
            if(IsInteractable(other, out var interactable) == false) return;
            _detectedInteractablesHolder.TryAddDetected(interactable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsInteractable(other, out var interactable)) return;
            _detectedInteractablesHolder.TryRemoveDetected(interactable);
        }

        private bool IsInteractable(Component other, out Interactable interactable)
        {
            var isSuccessful = other.TryGetComponent<Interactable>(out interactable);
            Debug.Log(isSuccessful);
            return isSuccessful;
        }
    }
}
