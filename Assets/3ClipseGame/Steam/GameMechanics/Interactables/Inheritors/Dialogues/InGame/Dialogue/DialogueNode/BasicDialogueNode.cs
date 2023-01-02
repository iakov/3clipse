using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue.DialogueNode
{
    public class BasicDialogueNode: DialogueNode
    {
        [SerializeField] private DialogueNode _nextNode;

        public override DialogueNode GetNextNode(Choice.Choice choice)
        {
            return _nextNode;
        }
    }
}