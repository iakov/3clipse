using _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue;
using _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Dialogue.DialogueNode;
// using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.UI
{
    public class DialogueEventsHandler
    {
        [SerializeField] private DialogueNodeChannel _dialogueNodeChannel;

        // [SerializeField] private TextMeshProUGUI _speakerText;
        // [SerializeField] private TextMeshProUGUI _dialogueText;

        [SerializeField] private RectTransform _choicesBoxTransform;
        [SerializeField] private DialogueUIController _choiceControllerPrefab;

        private bool _listenToInput = false;
        private DialogueNode _nextNode = null;
    }
}