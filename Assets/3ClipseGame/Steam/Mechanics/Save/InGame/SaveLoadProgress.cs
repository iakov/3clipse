using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame
{
    public class SaveLoadProgress : MonoBehaviour
    {
        [SerializeField] private ScenesLoader _scenesLoader;
        [SerializeField] private Image _loadingProgressBar;
        
        private void OnEnable()
        {
            _scenesLoader.LoadStarted += OnLoadStarted;
        }
        
        private void OnDisable()
        {
            _scenesLoader.LoadStarted -= OnLoadStarted;
        }
        
        private void OnLoadStarted()
        {
            StartCoroutine(DisplayLoadProgressRoutine());
        }
        
        private IEnumerator DisplayLoadProgressRoutine()
        {
            var progress = 0f;
            var scenesToLoad = _scenesLoader.ScenesToLoad;
        
            for (var i = 0; i < scenesToLoad.Count; i++)
            {
                while (!scenesToLoad[i].isDone)
                {
                    progress += scenesToLoad[i].progress;
                    _loadingProgressBar.fillAmount = progress/scenesToLoad.Count;
                    yield return null;
                }
            }
        }
    }
}
