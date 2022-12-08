using System.Collections;
using System.Globalization;
using _3ClipseGame.Steam.Mechanics.Save.InGame;
using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.GameIntroScene
{
    public class WaitAndSave : MonoBehaviour
    {
        [SerializeField] private float _waitTime;
        [SerializeField] private SceneObject _newLoadScene;
        [SerializeField] private TMP_Text _timerText;
        
        private float _time;
        
        private IEnumerator Start()
        {
            while (_time < _waitTime)
            {
                _timerText.text = _time.ToString(CultureInfo.InvariantCulture);
                _time += Time.deltaTime;
                yield return null;
            }
            
            SaveManager.Instance.ScenesLoader.LoadScene(_newLoadScene);
        }
    }
}
