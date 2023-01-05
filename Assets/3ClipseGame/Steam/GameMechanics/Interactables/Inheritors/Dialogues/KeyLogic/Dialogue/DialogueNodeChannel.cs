using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{    
    [CreateAssetMenu(fileName = "DialogueNodeChannel", menuName = "Interactables/Dialogue/DialogueNode/DialogueNodeChannel")]
    public class DialogueNodeChannel: ScriptableObject
    {
        public delegate void DialogueNodeCallback(DialogueNode.DialogueNode node);
    }
}