using System.Collections;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.CameraInput;
using _3ClipseGame.Steam.Core.GameSource.Parts.UI;
using Cinemachine;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.States.GameStates
{
    public abstract class GameMode : MonoBehaviour
    {
        [SerializeField] protected UIManager UIManager;
        [SerializeField] protected PointerManager PointerManager;
        [SerializeField] protected float TimeScale;
        [SerializeField] protected CursorLockMode CursorMode;
        
        [SerializeField] private CinemachineStateDrivenCamera _stateDrivenCamera;
        [SerializeField] private GameStateType modeType;

        public GameStateType GetModeType() => modeType;
        
        public abstract void StartEnter();
        public abstract void Exit();
        protected abstract void EndEnter();

        protected IEnumerator TrackBlendCompletion()
        {
            yield return null;
            while (_stateDrivenCamera.IsBlending) yield return null;
            EndEnter();
        }
    }
}
