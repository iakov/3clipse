using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode;
using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic
{
    public class DialogueEventsHandler: MonoBehaviour
    {
        [SerializeField] private TMP_Text _choice;
        [SerializeField] private DialogueNodeChannel _dialogueNodeChannel;

        private DialogueNode _choiceNextNode;
    }
}