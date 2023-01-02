using _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Narration;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue.DialogueNode.Choice
{
    [CreateAssetMenu(fileName = "DialogueChoice", menuName = "Dialogue/Choice")]
    public class Choice : ScriptableObject
    {
        private Speech _speech;
        private NarrationCharacter _asker;

        public bool IsEqual(Choice choice)
        {
            return choice._asker == _asker && choice._speech == _speech;
        }
    }
}