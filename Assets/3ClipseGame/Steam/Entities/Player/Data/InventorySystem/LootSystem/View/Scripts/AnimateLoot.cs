using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class AnimateLoot : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private float _minMoveMagnitude = 0.05f;
        [SerializeField] private float _maxMoveMagnitude = 0.1f;
        [SerializeField] private float _aboveGroundHeight = 0.1f;
        [SerializeField] private float _stepTime = 2f;

        #endregion

        #region PrivateFields

        private Rigidbody _rigidbody;
        private InactiveLootDisabler _inactiveLootDisabler;

        private bool _isMovingToTarget;
        
        private Vector3 _startPosition;
        private bool _isLastMoveUp;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inactiveLootDisabler = GetComponent<InactiveLootDisabler>();
        }
        
        private void Update()
        {
            if (!_rigidbody.isKinematic || _startPosition == Vector3.zero || _isMovingToTarget) return;

            StartCoroutine(StartMoveIteration(Random.Range(_minMoveMagnitude, _maxMoveMagnitude), _stepTime));
        }

        private void OnEnable()
        {
            _inactiveLootDisabler.LootDeactivated += SetPreAnimateVariables;
        }
        
        private void OnDisable()
        {
            _inactiveLootDisabler.LootDeactivated -= SetPreAnimateVariables;
        }

        #endregion

        #region PrivateMethods
        
        private void SetPreAnimateVariables()
        {
            _startPosition = transform.position;
            _startPosition.y += _aboveGroundHeight;
        }
        
        #endregion

        #region Coroutines

        private IEnumerator StartMoveIteration(float magnitude, float time)
        {
            _isMovingToTarget = true;

            if (_isLastMoveUp) LeanTween.moveY(gameObject, _startPosition.y - magnitude, time);
            else LeanTween.moveY(gameObject, _startPosition.y + magnitude, time);

            _isLastMoveUp = !_isLastMoveUp;
            
            yield return new WaitForSeconds(time);
            
            _isMovingToTarget = false;
        }

        #endregion
    }
}
