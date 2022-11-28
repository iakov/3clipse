using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts
{
    public class FirstScreenCamera : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _cameraSpeed;
        [SerializeField] private CinemachineDollyCart _cart;
        [SerializeField] private CinemachineSmoothPath _dollyTrack;

        public event Action MoveFinished; 

        public void MoveToPoint(float point)
        {
            StartCoroutine(MoveToPointRoutine(point));
        }

        private IEnumerator MoveToPointRoutine(float position)
        {
            var time = 0f;

            while (_cart.m_Position < position)
            {
                var currentSpeed = _cameraSpeed.Evaluate(time);
                _cart.m_Speed = currentSpeed;
                time += Time.deltaTime;
                yield return null;
            }
            
            MoveFinished?.Invoke();
        }
    }
}
