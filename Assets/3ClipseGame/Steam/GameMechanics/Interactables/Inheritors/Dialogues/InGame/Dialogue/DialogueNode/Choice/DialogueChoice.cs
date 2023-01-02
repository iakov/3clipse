using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue.DialogueNode.Choice
{
    [CreateAssetMenu(fileName = "DialogueChoice", menuName = "Dialogue/DialogueChoice")]
    public class DialogueChoice: ScriptableObject
    {
        [SerializeField] private Choice _transitionChoice;
        [SerializeField] private DialogueNode _choiceNode;
        public Choice TransitionChoice => _transitionChoice;
        public DialogueNode ChoiceNode => _choiceNode;
    }
}