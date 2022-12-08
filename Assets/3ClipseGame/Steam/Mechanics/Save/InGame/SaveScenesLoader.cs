using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    public class SaveScenesLoader : MonoBehaviour
    {
        [SerializeField] private SceneObject _currentScene;

        public List<AsyncOperation> ScenesToLoad = new();
        public event Action LoadFinished;
        public event Action LoadStarted;

        private SceneObject _currentlyLoadingScene;
        
        private void Awake()
        {
            _currentScene.Load(this);
        }

        public void LoadScene(SceneObject scene)
        {
            _currentlyLoadingScene = scene;
            ScenesToLoad = _currentlyLoadingScene.Load(this);
            LoadStarted?.Invoke();
            _currentlyLoadingScene.OperationFinished += UnloadPreviousScene;
        }

        private void UnloadPreviousScene()
        {
            _currentlyLoadingScene.OperationFinished -= UnloadPreviousScene;
            ScenesToLoad = _currentScene.Unload(this);
            _currentScene.OperationFinished += OnSceneOperationFinished;
        }

        private void OnSceneOperationFinished()
        {
            _currentScene.OperationFinished -= OnSceneOperationFinished;
            _currentScene = _currentlyLoadingScene;
            LoadFinished?.Invoke();
        }
    }
}