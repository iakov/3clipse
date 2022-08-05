using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Prefabs
{
    public class AnimateLoot : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private float minMoveMagnitude = 0.05f;
        [SerializeField] private float maxMoveMagnitude = 0.1f;
        [SerializeField] private float aboveGroundHeight = 0.1f;
        [SerializeField] private float stepTime = 2f;

        #endregion

        #region PrivateFields

        private Rigidbody _rigidbody;
        private DisableOnGround _lootDisabler;

        private bool _isMovingToTarget;
        
        private Vector3 _startPosition;
        private bool _isLastMoveUp;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _lootDisabler = GetComponent<DisableOnGround>();
        }

        private void OnEnable() => _lootDisabler.LootDeactivated += () => {
            _startPosition = transform.position;
            _startPosition.y += aboveGroundHeight;
        };

        private void Update()
        {
            if (!_rigidbody.isKinematic || _startPosition == Vector3.zero || _isMovingToTarget) return;

            StartCoroutine(StartMoveIteration(Random.Range(minMoveMagnitude, maxMoveMagnitude), stepTime));
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
