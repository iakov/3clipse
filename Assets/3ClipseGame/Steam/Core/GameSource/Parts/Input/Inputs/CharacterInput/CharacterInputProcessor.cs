using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.CharacterInput
{
    public class CharacterInputProcessor : InputProcessor
    {
        [SerializeField] private CharacterInputHandler _characterInputHandler;

        public bool GetIsEnvironmentInteracted() => _isEnvironmentInteracted;

        private bool _isEnvironmentInteracted;

        private void Awake()
        {
            _characterInputHandler.EnvironmentInteracted += OnEnvironmentInteracted;
        }
        
        public override void Enable()
        {
            _characterInputHandler.Enable();
        }

        public override void Disable()
        {
            _characterInputHandler.Disable();
        }

        private void OnEnvironmentInteracted()
            => StartCoroutine(EnvironmentInteractedDelay());

        private IEnumerator EnvironmentInteractedDelay()
        {
            _isEnvironmentInteracted = true;
            yield return null;
            _isEnvironmentInteracted = false;
        }
    }
}
