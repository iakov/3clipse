using UnityEngine;

namespace _3ClipseGame.Steam.Core.UI.Scripts.ElementsTweening
{
    public class ScaleAnimations : MonoBehaviour
    {
        [Header("Scale parameters")] [SerializeField]
        private Vector3 baseSize;

        [SerializeField] private Vector3 activeSize;
        [SerializeField] private float scaleTime;
        [SerializeField] private float unscaleTime;

        [Header("Sound parameters")] [SerializeField]
        private AudioClip activeClip;

        [SerializeField] private AudioClip inactiveClip;
        [SerializeField] private AudioSource audioSource;

        public void MakeBigger()
        {
            gameObject.LeanScale(activeSize, scaleTime);
            audioSource.clip = activeClip;
            audioSource.Play();
        }

        public void MakeSmaller()
        {
            gameObject.LeanScale(baseSize, unscaleTime);
            audioSource.clip = inactiveClip;
            audioSource.Play();
        }
    }
}
