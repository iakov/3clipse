using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Visuals
{
    public class AnimateLoot : MonoBehaviour
    {
        #region Serialization

        [SerializeField] private AnimationCurve _verticalMovementCurve;

        #endregion

        #region Initialization

        private Rigidbody _rigidbody;
        private InactiveLootDisabler _lootDisabler;
        
        private bool _isBusy;
        private Vector3 _startPosition;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _lootDisabler = GetComponent<InactiveLootDisabler>();
        }

        #endregion

        #region PreAnimate

        private void Start()
        {
            _isBusy = false;
            _startPosition = _rigidbody.position;
        }

        #endregion

        #region Animate

        private void Update()
        {
            if (_isBusy || _lootDisabler != null) return;
            StartMovement();
        }

        private void StartMovement()
        {
            StartCoroutine(StartMoveIteration());
        }

        private IEnumerator StartMoveIteration()
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

        #endregion
    }
}
