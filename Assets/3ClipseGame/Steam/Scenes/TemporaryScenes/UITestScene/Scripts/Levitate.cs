using System.Runtime.Serialization;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TemporaryScenes.UITestScene.Scripts
{
    public class Levitate : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _yPositionCurve;
        [SerializeField] [Range(0f, 360f)] private float _rotationSpeedPerSecond;

        private Transform _transform;

        private float _curveTime;
        private float _time;

        private Vector3 _startPosition;
        private Quaternion _startQuaternion;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            if (_yPositionCurve.length < 2)
                throw new SerializationException("AnimationCurve cannot contain less than 2 keys");

            SetParameters();
            Randomise();
        }

        private void SetParameters()
        {
            _curveTime = _yPositionCurve.keys[_yPositionCurve.length - 1].time;
            _startPosition = _transform.position;
        }

        private void Randomise()
        {
            _time = Random.Range(0f, _curveTime);
            var random = Random.Range(0f, 360f);
            var rotationVector = new Vector3(0f, _rotationSpeedPerSecond, 0f) * random;
            _transform.Rotate(rotationVector);
        }

        private void Update()
        {
            _time += Time.deltaTime;
            ChangePosition();
            ChangeRotation();
        }

        private void ChangePosition()
        {
            var fullLoops = Mathf.Floor(_time / _curveTime);
            var evaluateTime = _time - fullLoops * _curveTime;
            var value = _yPositionCurve.Evaluate(evaluateTime);

            var position = _startPosition + Vector3.up * value;
            transform.position = position;
        }

        private void ChangeRotation()
        {
            var rotationVector = new Vector3(0f, _rotationSpeedPerSecond, 0f) * Time.deltaTime;
            _transform.Rotate(rotationVector);
        }
    }
}
