using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice
{
    [CreateAssetMenu(fileName = "DialogueChoice", menuName = "Interactables/Dialogue/DialogueNode/Choice/DialogueChoice")]
    public class DialogueChoice: ScriptableObject
    {
        [SerializeField] private Choice _transitionChoice;
        [SerializeField] private DialogueNode _choiceNode;
        public Choice TransitionChoice => _transitionChoice;
        public DialogueNode ChoiceNode => _choiceNode;
    }
}