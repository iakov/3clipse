using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Core.GameSource;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    [CreateAssetMenu(fileName = "New Scene Data", menuName = "Save/Game Scene")]
    public class SceneObject : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField] private bool _isOriginRequired;
        [SerializeField] private bool _isActive;
        [SerializeField] private string _sceneName;
        [SerializeField] private SceneObject _originScene;
        
        [Header("Defaults")]
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Quaternion _startRotation;

        [Header("Global Info")] 
        [SerializeField] private string _sceneInformalName;

        public string SceneName => _sceneInformalName;
        
        public event Action OperationFinished;
        
        public List<AsyncOperation> Load(MonoBehaviour coroutineStarter)
        {
            var savesToLoad = new List<AsyncOperation>();
            
            if(_isOriginRequired) savesToLoad = savesToLoad.Concat(_originScene.Load(coroutineStarter)).ToList();
            savesToLoad.Add(SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive));
            coroutineStarter.StartCoroutine(WaitForFinish(savesToLoad));
            OperationFinished += FinishLoad;

            return savesToLoad;
        }

        private void FinishLoad()
        {
            if (_isActive) SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
            if(_isOriginRequired) ApplyDefaults();
            OperationFinished -= FinishLoad;
        }

        public List<AsyncOperation> Unload(MonoBehaviour coroutineStarter)
        {
            var savesToLoad = new List<AsyncOperation>();
            
            if(_isOriginRequired) savesToLoad = savesToLoad.Concat(_originScene.Unload(coroutineStarter)).ToList();
            if(SceneManager.GetSceneByName(_sceneName).isLoaded) savesToLoad.Add(SceneManager.UnloadSceneAsync(_sceneName));
            coroutineStarter.StartCoroutine(WaitForFinish(savesToLoad));

            return savesToLoad;
        }

        private IEnumerator WaitForFinish(List<AsyncOperation> operations)
        {
            foreach (var operation in operations)
            {
                while (operation.isDone == false) 
                    yield return null;
            }

            OperationFinished?.Invoke();
        }

        private void ApplyDefaults()
        {
            GameSource.Instance.GetPlayer().transform.position = _startPosition;
            GameSource.Instance.GetPlayer().transform.rotation = _startRotation;
        }
    }
}
