using _3ClipseGame.Steam.GameCore.Origin;
using _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Interactables/Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private DialogueNode.DialogueNode _firstNode;
        
        public DialogueNode.DialogueNode CurrentDialogueNode { get; private set; }
        public Choice Choice { get; set; }
        
        public DialogueNode.DialogueNode StartDialogue()
        {
            var stateManager = GameSource.Instance.GetStatesManager();
            stateManager.Enable(GameStateType.Cinematic);

            return _firstNode;
        }

        public void EndDialogue()
        {
            var stateManager = GameSource.Instance.GetStatesManager();
            stateManager.Enable(GameStateType.PlayMode);
        }

        public void StartDialogueNode(DialogueNode.DialogueNode node)
        {
	        var isCanStartDialogue = CanStartDialogueNode(node);
	        if (isCanStartDialogue == false) throw new DialogueException("Failed to start a dialogue node");

            if (node == null) node = CurrentDialogueNode.GetNextNode(null);
            EndDialogueNode(CurrentDialogueNode); 
            CurrentDialogueNode = node;
        }
	
	    private bool CanStartDialogueNode(DialogueNode.DialogueNode node)
        {
            bool isNextNode = false;
            if (CurrentDialogueNode != null) isNextNode = CurrentDialogueNode.GetNextNode(Choice) == node;
             
            var isNull = CurrentDialogueNode == null || node == null;
	        return isNextNode || isNull;
	    }

        public void EndDialogueNode(DialogueNode.DialogueNode node)
        {
            if (CurrentDialogueNode == node) CurrentDialogueNode = null;
            else throw new DialogueException("Trying to stop a dialogue node that isn't running!");
        }
    }
}