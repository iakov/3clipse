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

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _audioSource = GetComponent<AudioSource>();
        }

        #endregion

        #region PublicMethods

        public void PlaySound()
        {
            var ray = new Ray(_transform.position, Vector3.down);
            if(!Physics.Raycast(ray, out var raycastHit, rayDistance, soundsDetectLayer)) throw new ArgumentException();
            
            var groundMaterial = raycastHit.collider.gameObject.GetComponent<Renderer>().material;
            var sounds = soundsStorage.TryGetStepSounds(groundMaterial);

            var randomSound = sounds[Random.Range(0, sounds.Count - 1)];
            _audioSource.clip = randomSound;
            _audioSource.Play();
        }

        #endregion
    }
}
