using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueInstigator: MonoBehaviour
    {
        [SerializeField] private DialogueNodeChannel _dialogueNodeChannel;
        private Dialogue _currentDialogue;

        private void Awake()
        {
            _dialogueNodeChannel.OnDialogueNodeRequested += TryStartDialogueNode;
            _dialogueNodeChannel.OnDialogueChoiceReceived += SetChoice;
            _dialogueNodeChannel.OnDialogueEnd += TryEndCurrentDialogue;
        }

        private void OnDestroy()
        {
            _dialogueNodeChannel.OnDialogueNodeRequested -= TryStartDialogueNode;
            _dialogueNodeChannel.OnDialogueChoiceReceived -= SetChoice;
            _dialogueNodeChannel.OnDialogueEnd -= TryEndCurrentDialogue;
        }

        public void TryStartDialogue(Dialogue dialogue)
        {
            var isCanStartDialogue = CanStartDialogue(_currentDialogue);
            if (isCanStartDialogue == false) throw new DialogueException("Can't start dialogue when another one is running!");
            
            _currentDialogue = dialogue;
            var firstNode = _currentDialogue.StartDialogue();
            TryStartDialogueNode(firstNode);
        }

        private void TryEndDialogue(Dialogue dialogue)
        {
            var isCurrentDialogueEqualsDialogue = _currentDialogue == dialogue;
            if (isCurrentDialogueEqualsDialogue == false) throw new DialogueException("Trying to stop a dialogue that isn't running!");
            
            dialogue.EndDialogue();
                
            _dialogueNodeChannel.RaiseEndDialogueNode(dialogue.CurrentDialogueNode);
            TryEndDialogueNode(dialogue.CurrentDialogueNode);
                
            _currentDialogue = null;
        }

        private void TryStartDialogueNode(DialogueNode.DialogueNode node)
        {
            _dialogueNodeChannel.RaiseEndDialogueNode(_currentDialogue.CurrentDialogueNode);
            _currentDialogue.StartDialogueNode(node);

            var isDialogueNodeFinal = _currentDialogue.CurrentDialogueNode == null;
            if (isDialogueNodeFinal)
            {
                TryEndDialogue(_currentDialogue);
            }
            else
            {
                if (_currentDialogue.CurrentDialogueNode is ChoiceDialogueNode)
                    _dialogueNodeChannel.RaiseDisplayDialogueNode(_currentDialogue.CurrentDialogueNode);
                _dialogueNodeChannel.RaiseStartDialogueNode(_currentDialogue.CurrentDialogueNode);
            }
        }
        
        private void TryEndDialogueNode(DialogueNode.DialogueNode node) => _currentDialogue.EndDialogueNode(node);

        private void SetChoice(Choice choice) => _currentDialogue.Choice = choice;

        private void TryEndCurrentDialogue(Dialogue dialogue) => TryEndDialogue(_currentDialogue);

        private bool CanStartDialogue(Dialogue dialogue)
        {
            if (dialogue == null || dialogue.CurrentDialogueNode == null)
                return true;
            return false;
        }
    }
}