using System.Collections;
using _3ClipseGame.Steam.Global.Input;
using _3ClipseGame.Steam.Global.Input.Scripts;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using _3ClipseGame.Steam.Global.UI.Scripts;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace _3ClipseGame.Steam.Global.GameScripts.GameStates
{
    public abstract class GameMode : MonoBehaviour
    {
        #region Serialization

        [Header("Global Settings")]
        [SerializeField] protected UIManager uiManager;
        [SerializeField] protected PointerManager pointerManager;
        
        [Header("Time Settings")]
        [Range(0, 2)] [SerializeField] protected float timeScale;
        
        [Header("Camera Settings")]
        [SerializeField] protected CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] protected CameraAnimatorController cameraAnimatorController;
        
        [Header("Events")]
        [SerializeField] protected UnityEvent blendBegan;
        [SerializeField] private UnityEvent blendCompleted;

        #endregion

        #region Events

        public event UnityAction BlendBegan
        {
            add => blendBegan.AddListener(value);
            remove => blendBegan.RemoveListener(value);
        }

        public event UnityAction BlendCompleted
        {
            add => blendCompleted.AddListener(value);
            remove => blendCompleted.RemoveListener(value);
        }

        #endregion

        #region Coroutines

        protected IEnumerator TrackBlendCompletion(ICinemachineCamera stateCamera)
        {
            yield return null;
            while (stateDrivenCamera.IsBlending) yield return null;
            if (stateDrivenCamera.LiveChild != stateCamera) yield break; 
            blendCompleted?.Invoke();
        }

        #endregion

        #region AbstractMethods

        public abstract void StartEnable();
        public abstract void Disable();

        #endregion
    }
}

