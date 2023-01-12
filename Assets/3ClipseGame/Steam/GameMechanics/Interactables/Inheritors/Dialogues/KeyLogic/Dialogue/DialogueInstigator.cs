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
            // _dialogueNodeChannel.OnDialogueNodeEnd += TryEndDialogueNode;
            _dialogueNodeChannel.OnDialogueChoiceGot += SetChoice;
            _dialogueNodeChannel.OnDialogueEnd += TryEndCurrentDialogue;
        }

        private void OnDestroy()
        {
            _dialogueNodeChannel.OnDialogueNodeRequested -= TryStartDialogueNode;
            // _dialogueNodeChannel.OnDialogueNodeEnd -= TryEndDialogueNode;
            _dialogueNodeChannel.OnDialogueChoiceGot -= SetChoice;
            _dialogueNodeChannel.OnDialogueEnd -= TryEndCurrentDialogue;
        }

        public void TryStartDialogue(Dialogue dialogue)
        {
            if (_currentDialogue == null || _currentDialogue.CurrentDialogueNode == null)
            {
                _currentDialogue = dialogue;
                var firstNode = _currentDialogue.StartDialogue();
                TryStartDialogueNode(firstNode);
            }
            else
            {
                throw new DialogueException("Can't start dialogue when another one is running!");
            }
        }

        private void TryEndDialogue(Dialogue dialogue)
        {
            if (_currentDialogue == dialogue)
            {
                dialogue.EndDialogue();
                _dialogueNodeChannel.RaiseEndDialogueNode(dialogue.CurrentDialogueNode);
                TryEndDialogueNode(dialogue.CurrentDialogueNode);
                _currentDialogue = null;
            }
            else
            {
                throw new DialogueException("Trying to stop a dialogue that isn't running!");
            }
        }

        private void TryStartDialogueNode(DialogueNode.DialogueNode node)
        {
            _dialogueNodeChannel.RaiseEndDialogueNode(_currentDialogue.CurrentDialogueNode);
            _currentDialogue.StartDialogueNode(node);

            if (_currentDialogue.CurrentDialogueNode == null)
            {
                TryEndDialogue(_currentDialogue);
            }
            else
            {
                if (_currentDialogue.CurrentDialogueNode.GetType() == typeof(ChoiceDialogueNode)) 
                    _dialogueNodeChannel.RaiseDrawDialogueNode(_currentDialogue.CurrentDialogueNode);
                _dialogueNodeChannel.RaiseStartDialogueNode(_currentDialogue.CurrentDialogueNode);
            }
        }
        
        private void TryEndDialogueNode(DialogueNode.DialogueNode node) => _currentDialogue.EndDialogueNode(node);

        private void SetChoice(Choice choice) => _currentDialogue.Choice = choice;

        private void TryEndCurrentDialogue(Dialogue dialogue) => TryEndDialogue(_currentDialogue);
    }
}