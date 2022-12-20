using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    [CreateAssetMenu(fileName = "New Save Scenes Loader", menuName = "Save/Scenes Loader")]
    public class ScenesLoader : ScriptableObject
    {
        [SerializeField] private SceneObject _firstScene;
        private SceneObject _currentScene;

        public List<AsyncOperation> ScenesToLoad = new();
        public SceneObject CurrentScene => _currentScene;
        public event Action LoadFinished;
        public event Action LoadStarted;

        private SceneObject _currentlyLoadingScene;

        public void LoadDefault()
        {
            _currentlyLoadingScene = _firstScene;
            _currentScene = _firstScene;
            _firstScene.Load(InterSceneSavesEntry.Instance);
            _firstScene.OperationFinished += OnSceneOperationFinished;
        }

        public void LoadScene(SceneObject scene)
        {
            _currentlyLoadingScene = scene;
            ScenesToLoad = _currentlyLoadingScene.Load(InterSceneSavesEntry.Instance);
            LoadStarted?.Invoke();
            _currentlyLoadingScene.OperationFinished += UnloadPreviousScene;
        }

        private void UnloadPreviousScene()
        {
            _currentlyLoadingScene.OperationFinished -= UnloadPreviousScene;
            ScenesToLoad = _currentScene.Unload(InterSceneSavesEntry.Instance);
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