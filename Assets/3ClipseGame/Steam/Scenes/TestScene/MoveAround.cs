using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene
{
    public class MoveAround : MonoBehaviour
    {
        [SerializeField] private List<Transform> _pathPoints;
        [SerializeField] private float _moveSpeed;
        
        private event Action MovementFinished;
        private Transform _transform;
        
        private int _currentIndex;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            MovementFinished?.Invoke();
        }

        private void OnEnable()
        {
            MovementFinished += OnMovementFinished;
        }

        private void OnDisable()
        {
            MovementFinished -= OnMovementFinished;
        }

        private void OnMovementFinished()
        {
            StartCoroutine(SetNewMove());
        }

        private IEnumerator SetNewMove()
        {
            var newPosition =  GetNextPosition();
            var time = CalculateTime(newPosition);
            LeanTween.move(gameObject, newPosition, time);
            yield return new WaitForSeconds(time);
            MovementFinished?.Invoke();
        }

        private Vector3 GetNextPosition()
        {
            var newPosition = _pathPoints[_currentIndex].position;
            _currentIndex = _currentIndex == _pathPoints.Count - 1
                ? 0
                : _currentIndex + 1;
            return newPosition;
        }
        
        private float CalculateTime(Vector3 newLocation)
        {
            if (_moveSpeed == 0f) throw new SerializationException("Move speed cannot be zero");
            
            var distance = Vector3.Distance(_transform.position, newLocation);
            return distance / _moveSpeed;
        }
    }
}
