using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private DialogueNode.DialogueNode _firstNode;
        public DialogueNode.DialogueNode CurrentDialogueNode { get; private set; }

        public void Start()
        {
            CurrentDialogueNode = _firstNode;
        }
    }
}