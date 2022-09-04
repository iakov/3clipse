using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Scripts
{
    public class StepSounds : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private StepSoundsStorage soundsStorage;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask soundsDetectLayer;

        #endregion

        #region PrivateMethods

        private Transform _transform;
        private AudioSource _audioSource;
        private CapsuleCollider _capsuleCollider;

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _audioSource = GetComponent<AudioSource>();
            _capsuleCollider = GetComponentInParent<CapsuleCollider>();
        }

        #endregion

        #region PublicMethods

        public void PlaySound(float volume)
        {
            var position = _transform.position;
            var bottomPosition = position + _capsuleCollider.center + Vector3.down * _capsuleCollider.height / 2;
            
            var ray = new Ray(new Vector3(bottomPosition.x, bottomPosition.y, bottomPosition.z), Vector3.down);
            if (!Physics.Raycast(ray, out var raycastHit, rayDistance, soundsDetectLayer)) throw new ArgumentException("Cannot detect ground");
            
            var groundMaterial = raycastHit.collider.gameObject.GetComponent<Renderer>().material;
            var sounds = soundsStorage.TryGetStepSounds(groundMaterial);
            
            var randomSound = sounds[Random.Range(0, sounds.Count - 1)];
            _audioSource.clip = randomSound;
            _audioSource.volume = volume;
            _audioSource.Play();
        }

        #endregion
    }
}
