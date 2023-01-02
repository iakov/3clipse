using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Narration
{
    [CreateAssetMenu(fileName = "Speech", menuName = "Dialogue/Narration/Speech")]
    public class Speech: ScriptableObject
    {
        [SerializeField] private string _speechText;
        public string SpeechText => _speechText;
    }
}