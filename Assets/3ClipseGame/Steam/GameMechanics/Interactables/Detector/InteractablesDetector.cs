using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Detector
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(DetectedInteractablesHolder))]
    
    public class InteractablesDetector : MonoBehaviour
    {
        private DetectedInteractablesHolder _detectedInteractablesHolder;

        private void Awake()
        {
            _detectedInteractablesHolder = GetComponent<DetectedInteractablesHolder>();
            GetComponent<SphereCollider>().isTrigger = true;
        }

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
            return isSuccessful;
        }
    }
}
