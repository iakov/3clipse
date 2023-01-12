using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice;
using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic
{
    public class DialogueUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _speakerName;
        [SerializeField] private TMP_Text _dialogueText;

        [SerializeField] private RectTransform _choicesBoxTransform;
        [SerializeField] private DialogueChoiceController _choiceControllerPrefab;

        [SerializeField] private DialogueNodeChannel _dialogueNodeChannel;

        private void Awake()
        {
            _dialogueNodeChannel.OnDialogueNodeStarted += OnDialogueNodeStarted;
            _dialogueNodeChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;
            _dialogueNodeChannel.OnDialogueNodeDraw += VisitedByChoiceNode;
            _dialogueNodeChannel.OnDialogueEnd += OnDialogueEnd;

            _choicesBoxTransform.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _dialogueNodeChannel.OnDialogueNodeStarted -= OnDialogueNodeStarted;
            _dialogueNodeChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd;
            _dialogueNodeChannel.OnDialogueNodeDraw -= VisitedByChoiceNode;
            _dialogueNodeChannel.OnDialogueEnd -= OnDialogueEnd;
        }

        private void OnDialogueNodeStarted(DialogueNode node)
        {
            _speakerName.text = node.DialogueLine.DialogueSpeaker;
            _dialogueText.text = node.DialogueLine.DialogueText;
        }

        private void OnDialogueNodeEnd(DialogueNode node)
        {
            _speakerName.text = "";
            _dialogueText.text = "";

            foreach (Transform child in _choicesBoxTransform)
            {
                Destroy(child.gameObject);
            }

            _choicesBoxTransform.gameObject.SetActive(false);
        }

        private void VisitedByChoiceNode(DialogueNode node)
        {
            var choiceDialogueNode = (ChoiceDialogueNode)node;
            _choicesBoxTransform.gameObject.SetActive(true);

            foreach (DialogueChoice dialogueChoice in choiceDialogueNode.NextNodes)
            {
                DialogueChoiceController newChoice = Instantiate(_choiceControllerPrefab, _choicesBoxTransform);
                newChoice.DialogueChoice = dialogueChoice;
            }
        }

        private void OnDialogueEnd(Dialogue.Dialogue dialogue) => OnDialogueNodeEnd(null);

        public void VisitedByBasicNode()
        {
            if (!_choicesBoxTransform.gameObject.activeSelf)
                _dialogueNodeChannel.RaiseRequestDialogueNode(null);
        }
    }
}