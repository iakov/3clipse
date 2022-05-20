using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Globals.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        #region Initialization

        [SerializeField] private CameraStatesDictionary cameraStatesDictionary;
        [SerializeField] private Animator cameraAnimator;

        #endregion

        #region PublicMethods

        public void SwitchCamera(CameraType cameraType)
        {
            var stateName = cameraStatesDictionary.FindStateNameByType(cameraType);
            cameraAnimator.Play(stateName);
        }

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
