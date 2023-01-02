using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration
{
    [CreateAssetMenu(fileName = "NarrationLine", menuName = "Dialogue/Narration/NarrationLine")]
    public class NarrationLine : ScriptableObject
    {
        [SerializeField] private NarrationCharacter _speakerName;
        [SerializeField] private Speech _speech;
        public string DialogueSpeaker => _speakerName.CharacterName;
        public string DialogueText => _speech.SpeechText;
    }
}
