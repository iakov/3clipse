using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice
{
    [CreateAssetMenu(fileName = "DialogueChoice", menuName = "Dialogue/Choice")]
    public class Choice : ScriptableObject
    {
        private Speech _speech;
        private NarrationCharacter _asker;
        public Speech Speech => _speech;

        public bool IsEqual(Choice choice)
        {
            return choice._asker == _asker && choice._speech == _speech;
        }
    }
}