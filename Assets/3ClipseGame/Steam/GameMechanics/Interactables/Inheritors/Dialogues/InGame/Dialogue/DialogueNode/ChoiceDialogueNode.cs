using _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue.DialogueNode.Choice;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue.DialogueNode
{
    public class ChoiceDialogueNode: DialogueNode
    {
        [SerializeField] private DialogueChoice[] _nextNodes;
        
        public override DialogueNode GetNextNode(Choice.Choice choice)
        {
            foreach (var node in _nextNodes)
            {
                var nodeChoice = node.TransitionChoice;
                if (nodeChoice.IsEqual(choice)) return node.ChoiceNode;
            }

            throw new DialogueException("This node is not a continuation of the current one!");
        }
    }
}