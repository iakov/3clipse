using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode
{
    public abstract class DialogueNode: ScriptableObject
    {
        [SerializeField] private NarrationLine _dialogueLine;
        public NarrationLine DialogueLine => _dialogueLine;
        public abstract DialogueNode GetNextNode(Choice.Choice choice);
    }
}