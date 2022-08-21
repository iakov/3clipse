using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts
{
    public class RandomizeTaunt : StateMachineBehaviour
    {
        #region Initialization

        [SerializeField] private float minTimeToTaunt = 10f;
        [SerializeField] private float maxTimeToTaunt = 20f;

        private static readonly int IsTaunted = Animator.StringToHash("IsTaunted");
        
        private float _randomTimeToTaunt;
        private float _time;
        private bool _isSwitching;

        #endregion

        #region StateMachineMethods

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _randomTimeToTaunt = Random.Range(minTimeToTaunt, maxTimeToTaunt);
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _time += Time.deltaTime;

            if (_time < _randomTimeToTaunt || _isSwitching) return;

            _isSwitching = true;
            animator.SetTrigger(IsTaunted);
        }
        

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _time = 0f;
            _isSwitching = false;
        }

        #endregion
    }
}
