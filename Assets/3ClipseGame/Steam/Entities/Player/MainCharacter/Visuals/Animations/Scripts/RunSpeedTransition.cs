using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Animations.Scripts
{
    public class RunSpeedTransition : StateMachineBehaviour
    {
        #region SerializableFields

        [SerializeField] private float _maxSpeed = 1;
        [SerializeField] private float _minSpeed = 0;
        [SerializeField] private float _speedDownModifier = 2;
        [SerializeField] private float _speedUpModifier = 1;

        #endregion

        #region StatesNameHash

        private static readonly int _walkSpeed = Animator.StringToHash("WalkSpeed");
        private static readonly int _isRunning = Animator.StringToHash("IsRunning");

        #endregion

        #region StateMachineBehaviourMethods

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var isRunning = animator.GetBool(_isRunning);
            var currentSpeed = animator.GetFloat(_walkSpeed);
        
            if (isRunning) currentSpeed += Time.deltaTime * _speedUpModifier;
            else currentSpeed -= Time.deltaTime * _speedDownModifier;

            if (currentSpeed > _maxSpeed) currentSpeed = _maxSpeed;
            if (currentSpeed < _minSpeed) currentSpeed = _minSpeed;
        
            animator.SetFloat(_walkSpeed, currentSpeed);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
            => animator.SetFloat(_walkSpeed, _minSpeed);
        

        #endregion
    }
}
