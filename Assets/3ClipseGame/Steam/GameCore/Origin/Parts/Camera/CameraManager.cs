using System.Collections.Generic;
using _3ClipseGame.Steam.GameCore.Origin.Interfaces;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Camera.GameCameras;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Camera
{
    public class CameraManager : MonoBehaviour, ISoloManager<CameraType>
    {
        [SerializeField] private List<GameCamera> _gameCameras;
        private GameCamera _currentCamera;
        
        public void Enable(CameraType enableObjectType)
        {
            _currentCamera = FindCameraWithType(enableObjectType);
            _currentCamera.Enable();
        }

        private GameCamera FindCameraWithType(CameraType type)
        {
            return _gameCameras.Find(gameCamera => gameCamera.GetCameraType() == type);
        }

        public CameraType[] GetActive()
        {
            return new CameraType[] { _currentCamera.GetCameraType() };
        }
    }
}
