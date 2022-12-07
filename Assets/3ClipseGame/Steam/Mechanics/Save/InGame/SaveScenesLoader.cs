using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    public class SaveScenesLoader : MonoBehaviour
    {
        [SerializeField] private string _originSceneName = "OriginScene";
        [SerializeField] private string _dontDestroyOnLoadSceneName = "DontDestroyOnLoad";
        [SerializeField] private string _saveManagerSceneName = "SaveManagerScene";

        public event Action LoadStarted;
        public List<AsyncOperation> ScenesToLoad = new();

        private string _currentSaveSceneName;
        private EditorApplication.CallbackFunction _callback;

        public void LoadSaveScene(string saveSceneName, EditorApplication.CallbackFunction callback)
        {
            var saveScene = SceneManager.GetSceneByName(saveSceneName);
            if (saveScene.IsValid()) throw new ArgumentException($"Cannot find {saveSceneName} scene");
            
            _currentSaveSceneName = saveSceneName;
            _callback = callback;
            LoadScenes();
        }

        private void LoadScenes()
        {
            UnloadCurrentScene();
            LoadScene(_currentSaveSceneName);
            LoadOriginScene();
            LoadStarted?.Invoke();

            TryInvokeCallback(SceneManager.GetSceneByName(_dontDestroyOnLoadSceneName), LoadSceneMode.Additive);
            SceneManager.sceneLoaded += TryInvokeCallback;
        }
        
        private void UnloadCurrentScene()
        {
            var currentSaves = GetLoadedScenes();
            foreach (var save in currentSaves) 
                SceneManager.UnloadSceneAsync(save.name);
        }

        private List<Scene> GetLoadedScenes()
        {
            var currentLoadedScenes = SceneManager.GetAllScenes().ToList();
            currentLoadedScenes.Remove(SceneManager.GetSceneByName(_dontDestroyOnLoadSceneName));
            currentLoadedScenes.Remove(SceneManager.GetSceneByName(_originSceneName));
            currentLoadedScenes.Remove(SceneManager.GetSceneByName(_saveManagerSceneName));
            
            return currentLoadedScenes;
        }

        private void LoadOriginScene()
        {
            var currentSavesNames = SceneManager.GetAllScenes().ToList();
            var scene = SceneManager.GetSceneByName(_originSceneName);
            
            if (currentSavesNames.Contains(scene)) TryInvokeCallback(scene, LoadSceneMode.Additive);
            else ScenesToLoad.Add(SceneManager.LoadSceneAsync(_originSceneName, LoadSceneMode.Additive));
        }

        private void LoadScene(string sceneName)
        {
            var currentSavesNames = SceneManager.GetAllScenes().ToList();
            var scene = SceneManager.GetSceneByName(sceneName);
            
            if (currentSavesNames.Contains(SceneManager.GetSceneByName(sceneName))) TryInvokeCallback(scene, LoadSceneMode.Additive);
            else ScenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        }

        private void TryInvokeCallback(Scene scene, LoadSceneMode loadMode)
        {
            var originScene = SceneManager.GetSceneByName(_originSceneName);
            var saveScene = SceneManager.GetSceneByName(_currentSaveSceneName);
            
            if (originScene.isLoaded && saveScene.isLoaded) _callback?.Invoke();
        }
    }
}