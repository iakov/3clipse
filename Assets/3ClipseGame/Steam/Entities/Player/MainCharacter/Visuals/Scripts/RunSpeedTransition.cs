using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts
{
    public class RunSpeedTransition : StateMachineBehaviour
    {
        #region SerializableFields

        [SerializeField] private float maxSpeed = 1;
        [SerializeField] private float minSpeed = 0;
        [SerializeField] private float speedDownModifier = 2;
        [SerializeField] private float speedUpModifier = 1;

        #endregion

        #region StatesNameHash

        private static readonly int WalkSpeed = Animator.StringToHash("WalkSpeed");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        #endregion

        #region StateMachineBehaviourMethods

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var isRunning = animator.GetBool(IsRunning);
            var currentSpeed = animator.GetFloat(WalkSpeed);
        
            if (isRunning) currentSpeed += Time.deltaTime * speedUpModifier;
            else currentSpeed -= Time.deltaTime * speedDownModifier;

            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
            if (currentSpeed < minSpeed) currentSpeed = minSpeed;
        
            animator.SetFloat(WalkSpeed, currentSpeed);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.SetFloat(WalkSpeed, minSpeed);
        

        #endregion
    }
}
