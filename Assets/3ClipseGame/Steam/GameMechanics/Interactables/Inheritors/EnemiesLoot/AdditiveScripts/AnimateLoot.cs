using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.AdditiveScripts
{
    public class AnimateLoot : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _verticalMovementCurve;

        private Rigidbody _rigidbody;
        private InactiveLootDisabler _lootDisabler;

        private bool _isBusy;
        private Vector3 _startPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _lootDisabler = GetComponent<InactiveLootDisabler>();
        }

        private void Start()
        {
            _isBusy = false;
            _startPosition = _rigidbody.position;
        }

        private void Update()
        {
            if (_isBusy || _lootDisabler != null) return;
            StartMovement();
        }

        private void StartMovement() => StartCoroutine(MoveIterationRoutine());

        private IEnumerator MoveIterationRoutine()
        {
            _isBusy = true;
            yield return DoMoveIteration();
            _isBusy = false;
        }

        private IEnumerator DoMoveIteration()
        {
            var time = 0f;
            var maxTime = GetCurveDuration(_verticalMovementCurve);

            while (time < maxTime)
            {
                MoveToNewPosition(time);
                time += Time.deltaTime;
                yield return null;
            }
        }

        private float GetCurveDuration(AnimationCurve curve)
        {
            var lastKeyIndex = curve.length - 1;
            return curve[lastKeyIndex].time;
        }

        private void MoveToNewPosition(float timeFromStart)
        {
            var newPosition = _startPosition + Vector3.up * _verticalMovementCurve.Evaluate(timeFromStart);
            _rigidbody.MovePosition(newPosition);
        }
    }
}
