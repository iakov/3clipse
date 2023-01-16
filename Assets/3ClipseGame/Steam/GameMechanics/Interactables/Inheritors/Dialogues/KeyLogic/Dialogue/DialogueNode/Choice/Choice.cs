using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice
{
    [CreateAssetMenu(fileName = "DialogueChoice", menuName = "Interactables/Dialogue/DialogueNode/Choice/Choice")]
    public class Choice : ScriptableObject
    {
        [SerializeField] private Speech _speech;
        [SerializeField] private NarrationCharacter _asker;
        public Speech Speech => _speech;

        public bool IsEqual(Choice choice)
        {
            return choice._asker == _asker && choice._speech == _speech;
        }
    }
}