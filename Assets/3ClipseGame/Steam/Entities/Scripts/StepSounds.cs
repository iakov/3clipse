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
            var position = _transform.position;
            var ray = new Ray(new Vector3(position.x, position.y + 0.2f, position.z), Vector3.down);
            if(!Physics.Raycast(ray, out var raycastHit, rayDistance)) Debug.Log("Hmmm");
            
            var groundMaterial = raycastHit.collider.gameObject.GetComponent<Renderer>().material;
            var sounds = soundsStorage.TryGetStepSounds(groundMaterial);
            
            var randomSound = sounds[Random.Range(0, sounds.Count - 1)];
            _audioSource.clip = randomSound;
            _audioSource.Play();
        }

        #endregion
    }
}
