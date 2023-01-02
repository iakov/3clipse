using _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Narration;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue.DialogueNode
{
    [CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode")]
    public abstract class DialogueNode: ScriptableObject
    {
        [SerializeField] private NarrationLine _dialogueLine;
        public NarrationLine DialogueLine => _dialogueLine;
        public abstract DialogueNode GetNextNode(Choice.Choice choice);
    }
}