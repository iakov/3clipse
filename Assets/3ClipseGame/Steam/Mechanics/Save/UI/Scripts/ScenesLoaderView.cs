using System.Collections;
using _3ClipseGame.Steam.Mechanics.Save.InGame;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using Packages.LeanTween.Presets;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts
{
    public class ScenesLoaderView : MonoBehaviour
    {
        [SerializeField] private ScenesLoader _scenesLoader;
        [SerializeField] private FadeInScript _fadeInScript;

        public SceneObject CurrentScene => _scenesLoader.CurrentScene;
        private bool _isPreviousFinished = true;
        private GameSave _loadingSave;

        private void Start()
        {
            _scenesLoader.LoadDefault();
        }

        private void OnEnable() => _scenesLoader.LoadFinished += OnLoadFinished;

        private void OnDisable() => _scenesLoader.LoadFinished -= OnLoadFinished;

        public void Load(GameSave save)
        {
            StartCoroutine(LoadScreenAppearRoutine(save.SceneObject));
            _loadingSave = save;
            _scenesLoader.LoadFinished += ApplySaveData;
        }

        public void Load(SceneObject sceneObject)
        {
            StartCoroutine(LoadScreenAppearRoutine(sceneObject));
        }

        private IEnumerator LoadScreenAppearRoutine(SceneObject sceneObject)
        {
            while (_isPreviousFinished == false) yield return null;
            
            _isPreviousFinished = false;
            yield return _fadeInScript.FadeIn();
            _isPreviousFinished = true;
            _scenesLoader.LoadScene(sceneObject);
        }

        private void OnLoadFinished()
        {
            StartCoroutine(LoadScreenDisappearRoutine());
        }

        private void ApplySaveData()
        {
            _scenesLoader.LoadFinished -= ApplySaveData;
            _loadingSave.ApplySaveDataToScene();
        }

        private IEnumerator LoadScreenDisappearRoutine()
        {
            while (_isPreviousFinished == false) yield return null;
            
            _isPreviousFinished = false;
            yield return _fadeInScript.FadeOut();
            _isPreviousFinished = true;
        }
    }
}
