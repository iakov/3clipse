using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.StartScreen.Scripts
{
    public class FirstScreenCamera : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _cameraSpeed;
        [SerializeField] private CinemachineDollyCart _cart;

        public event Action MoveFinished; 

        public void MoveToPoint(float point)
        {
            if (Math.Abs(_cart.m_Position - point) > 0.05f) StartCoroutine(MoveToPointRoutine(point));
            else MoveFinished?.Invoke();
        }

        private IEnumerator MoveToPointRoutine(float position)
        {
            var time = 0f;

            while (Mathf.Abs(_cart.m_Position - position) > 0.05f)
            {
                var currentSpeed = _cameraSpeed.Evaluate(time);
                _cart.m_Speed = currentSpeed;
                time += Time.deltaTime;
                yield return null;
            }

            _cart.m_Position = position;
            _cart.m_Speed = 0f;
            MoveFinished?.Invoke();
        }
    }
}
