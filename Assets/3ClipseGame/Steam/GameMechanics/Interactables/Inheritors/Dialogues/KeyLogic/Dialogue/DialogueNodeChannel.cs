using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{    
    [CreateAssetMenu(fileName = "DialogueNodeChannel", menuName = "Interactables/Dialogue/DialogueNode/DialogueNodeChannel")]
    public class DialogueNodeChannel: ScriptableObject
    {
        public delegate void DialogueNodeCallback(DialogueNode.DialogueNode node);

        public delegate void DialogueChoiceCallback(Choice dialogueChoice);

        public delegate void DialogueCallback(Dialogue dialogue);

        public DialogueNodeCallback OnDialogueNodeRequested;
        public DialogueNodeCallback OnDialogueNodeStarted;
        public DialogueNodeCallback OnDialogueNodeEnd;
        public DialogueNodeCallback OnDialogueNodeDraw;

        public DialogueChoiceCallback OnDialogueChoiceGot;

        public DialogueCallback OnDialogueEnd;

        public void RaiseRequestDialogueNode(DialogueNode.DialogueNode node) => OnDialogueNodeRequested?.Invoke(node);
        
        public void RaiseStartDialogueNode(DialogueNode.DialogueNode node) => OnDialogueNodeStarted?.Invoke(node);
        
        public void RaiseEndDialogueNode(DialogueNode.DialogueNode node) => OnDialogueNodeEnd?.Invoke(node);

        public void RaiseDrawDialogueNode(DialogueNode.DialogueNode node) => OnDialogueNodeDraw?.Invoke(node);

        public void RaiseGetDialogueChoice(Choice choice) => OnDialogueChoiceGot?.Invoke(choice);

        public void RaiseEndDialogue(Dialogue dialogue) => OnDialogueEnd?.Invoke(dialogue);
    }
}