using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Interactables/Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private DialogueNode.DialogueNode _firstNode;
        public DialogueNode.DialogueNode CurrentDialogueNode { get; private set; }

        public void StartDialogue()
        {
            CurrentDialogueNode = _firstNode;
            Debug.Log(CurrentDialogueNode);
        }
    }
}