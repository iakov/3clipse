using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueInstigator: MonoBehaviour
    {
        [SerializeField] private DialogueNodeChannel _dialogueNodeChannel;
        private Dialogue CurrentDialogue;

        private void Awake()
        {
            _dialogueNodeChannel.OnDialogueNodeRequested += TryStartDialogueNode;
            _dialogueNodeChannel.OnDialogueNodeEnd += TryEndDialogueNode;
        }

        private void OnDestroy()
        {
            _dialogueNodeChannel.OnDialogueNodeRequested -= TryStartDialogueNode;
            _dialogueNodeChannel.OnDialogueNodeEnd -= TryEndDialogueNode; 
        }

        public void TryStartDialogue(Dialogue dialogue)
        {
            if (CurrentDialogue == null || CurrentDialogue.CurrentDialogueNode == null)
            {
                CurrentDialogue = dialogue;
                var firstNode = CurrentDialogue.StartDialogue();
                TryStartDialogueNode(firstNode);
            }
            else
            {
                throw new DialogueException("Can't start dialogue when another one is running!");
            }
        }

        public void TryEndDialogue(Dialogue dialogue)
        {
            if (CurrentDialogue == dialogue)
            {
                CurrentDialogue.EndDialogue();
                CurrentDialogue = null;
            }
            else
            {
                throw new DialogueException("Trying to stop a dialogue that isn't running!");
            }
        }

        public void TryStartDialogueNode(DialogueNode.DialogueNode node)
        {
            CurrentDialogue.StartDialogueNode(node);
            _dialogueNodeChannel.RaiseStartDialogueNode(CurrentDialogue.CurrentDialogueNode);
        }
        
        public void TryEndDialogueNode(DialogueNode.DialogueNode node)
        {
            CurrentDialogue.EndDialogueNode(node);
            _dialogueNodeChannel.RaiseEndDialogueNode(CurrentDialogue.CurrentDialogueNode);
        }
    }
}