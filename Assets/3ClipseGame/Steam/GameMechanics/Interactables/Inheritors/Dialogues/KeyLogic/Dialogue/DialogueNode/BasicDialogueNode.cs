using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode
{
    [CreateAssetMenu(fileName = "BasicDialogueNode", menuName = "Interactables/Dialogue/DialogueNode/BasicNode")]
    public class BasicDialogueNode: DialogueNode
    {
        [SerializeField] private DialogueNode _nextNode;

        public override DialogueNode GetNextNode(Choice.Choice choice)
        {
            return _nextNode;
        }
    }
}