using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame
{
    public static class SaveScenesLoader
    {
        private const string OriginSceneName = "OriginScene";
        private const string DontDestroyOnLoadSceneName = "DontDestroyOnLoad";
        
        private static string _currentSaveSceneName;
        private static EditorApplication.CallbackFunction _callback;

        public static void LoadSaveScene(string saveSceneName, EditorApplication.CallbackFunction callback)
        {
            var saveScene = SceneManager.GetSceneByName(saveSceneName);
            if (saveScene.IsValid()) 
                throw new ArgumentException($"Cannot find {saveSceneName} scene");
            _currentSaveSceneName = saveSceneName;
            _callback = callback;
            
            UnloadCurrentScene();
            LoadScene(saveSceneName);
            LoadOriginScene();

            OnSceneLoaded(SceneManager.GetSceneByName(DontDestroyOnLoadSceneName), LoadSceneMode.Additive);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private static void UnloadCurrentScene()
        {
            var currentSavesNames = SceneManager.GetAllScenes().ToList();
            currentSavesNames.Remove(SceneManager.GetSceneByName(DontDestroyOnLoadSceneName));
            currentSavesNames.Remove(SceneManager.GetSceneByName(OriginSceneName));

            var currentSaveName = currentSavesNames[0].name;
            SceneManager.UnloadSceneAsync(currentSaveName);
        }

        private static void LoadOriginScene()
        {
            var currentSavesNames = SceneManager.GetAllScenes().ToList();
            var scene = SceneManager.GetSceneByName(OriginSceneName);
            
            if (currentSavesNames.Contains(scene))
            {
                OnSceneLoaded(scene, LoadSceneMode.Additive);
                return;
            }
            SceneManager.LoadSceneAsync(OriginSceneName, LoadSceneMode.Additive);
        }

        private static void LoadScene(string sceneName)
        {
            var currentSavesNames = SceneManager.GetAllScenes().ToList();
            var scene = SceneManager.GetSceneByName(sceneName);
            
            if (currentSavesNames.Contains(SceneManager.GetSceneByName(sceneName)))
            {
                OnSceneLoaded(scene, LoadSceneMode.Additive);
                return;
            }
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            var originScene = SceneManager.GetSceneByName(OriginSceneName);
            var saveScene = SceneManager.GetSceneByName(_currentSaveSceneName);
            
            if (originScene.isLoaded && saveScene.isLoaded) _callback?.Invoke();
        }
    }
}