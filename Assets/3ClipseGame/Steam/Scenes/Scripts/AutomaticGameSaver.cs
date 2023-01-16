using System.Collections;
using _3ClipseGame.Steam.GameCore.Origin;
using _3ClipseGame.Steam.GameMechanics.GameSaves.InGame;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.Scripts
{
    public class AutomaticGameSaver : MonoBehaviour
    {
        [Min(1f)] [SerializeField] private float _saveFrequencySeconds = 5f;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_saveFrequencySeconds);
                if (GameSource.Instance != null) InterSceneSavesEntry.Instance.SaveGame();
            }
        }
    }
}
