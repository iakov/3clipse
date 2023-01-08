using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{    
    [CreateAssetMenu(fileName = "DialogueNodeChannel", menuName = "Interactables/Dialogue/DialogueNode/DialogueNodeChannel")]
    public class DialogueNodeChannel: ScriptableObject
    {
        public delegate void DialogueNodeCallback(DialogueNode.DialogueNode node);

        public DialogueNodeCallback OnDialogueNodeRequested;
        public DialogueNodeCallback OnDialogueNodeStarted;
        public DialogueNodeCallback OnDialogueNodeEnd;

        public void RaiseRequestDialogueNode(DialogueNode.DialogueNode node) => OnDialogueNodeRequested?.Invoke(node);
        
        public void RaiseStartDialogueNode(DialogueNode.DialogueNode node) => OnDialogueNodeStarted?.Invoke(node);
        
        public void RaiseEndDialogueNode(DialogueNode.DialogueNode node) => OnDialogueNodeEnd?.Invoke(node);
    }
}