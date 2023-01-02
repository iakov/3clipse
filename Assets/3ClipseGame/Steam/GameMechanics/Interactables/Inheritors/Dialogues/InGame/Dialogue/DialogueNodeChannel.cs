using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue
{    
    [CreateAssetMenu(fileName = "DialogueNodeChannel", menuName = "Dialogue/DialogueNodeChannel")]
    public class DialogueNodeChannel: ScriptableObject
    {
        public delegate void DialogueNodeCallback(DialogueNode.DialogueNode node);
    }
}