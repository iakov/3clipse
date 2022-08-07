using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.StateDrivenCamera
{
    public class CameraAnimatorController : MonoBehaviour
    {
        #region Initialization

        [SerializeField] private CameraStatesDictionary cameraStatesDictionary;
        
        private Animator _cameraAnimator;

        private float _beforeDisableXCameraSpeed;
        private float _beforeDisableYCameraSpeed;

        private string _currentStateName;

        #endregion

        #region MonoBehaviourMethods

        private void Awake(){
            _currentStateName = cameraStatesDictionary.FindStateNameByType(CameraType.MainCharacter);
            _cameraAnimator = GetComponent<Animator>();
        }

        #endregion

        #region PublicMethods

        public void SwitchCamera(CameraType cameraType)
        {
            var stateName = cameraStatesDictionary.FindStateNameByType(cameraType);
            _cameraAnimator.Play(stateName);
            _currentStateName = stateName;
        }

        public CameraType GetCurrentStateName() => cameraStatesDictionary.FindTypeByName(_currentStateName);

        #endregion

        #region PrivateStructs

        public enum CameraType
        {
            MainCharacter,
            Animal,
            MainMenu
        }

        [Serializable]
        private struct CameraState
        {
            public CameraType cameraType;
            public string stateName;
        }

        [Serializable]
        private struct CameraStatesDictionary
        {
            [SerializeField] private List<CameraState> cameraStates;

            public string FindStateNameByType(CameraType cameraType)
            {
                foreach (var cameraState in cameraStates.Where(cameraState => cameraState.cameraType == cameraType))
                    return cameraState.stateName;
                throw new ArgumentException("Camera Type not implemented");
            }

            public CameraType FindTypeByName(string name)
            {
                foreach (var cameraState in cameraStates.Where(cameraState => cameraState.stateName == name))
                    return cameraState.cameraType;
                throw new ArgumentException("Incorrect name argument");
            }
        }

        #endregion
    }
}
