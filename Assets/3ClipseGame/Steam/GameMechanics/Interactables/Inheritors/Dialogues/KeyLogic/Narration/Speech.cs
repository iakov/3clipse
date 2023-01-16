using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration
{
    [CreateAssetMenu(fileName = "Speech", menuName = "Interactables/Dialogue/Narration/Speech")]
    public class Speech: ScriptableObject
    {
        [SerializeField] private string _speechText;
        public string SpeechText => _speechText;
    }
}