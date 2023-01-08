using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice;
using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic
{
    public class DialogueUIController: MonoBehaviour
    {
        [SerializeField] private TMP_Text _speakerName;
        [SerializeField] private TMP_Text _dialogueText;

        [SerializeField] private RectTransform _choicesBoxTransform;
        [SerializeField] private DialogueChoiceController _choiceControllerPrefab;

        [SerializeField] private DialogueNodeChannel _dialogueNodeChannel;

        private bool _listenToInput = false;
        private DialogueNode _nextNode = null;

        private void Awake()
        {
            _dialogueNodeChannel.OnDialogueNodeStarted += OnDialogueNodeStarted;
            _dialogueNodeChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;
            
            _choicesBoxTransform.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _dialogueNodeChannel.OnDialogueNodeStarted -= OnDialogueNodeStarted;
            _dialogueNodeChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd; 
        }

        private void Update()
        {
            // if (_listenToInput)
        }

        private void OnDialogueNodeStarted(DialogueNode node)
        {
            _speakerName.text = node.DialogueLine.DialogueSpeaker;
            _dialogueText.text = node.DialogueLine.DialogueText;
        }

        private void OnDialogueNodeEnd(DialogueNode node)
        {
            _nextNode = null;
            _listenToInput = false;
            _speakerName.text = "";
            _dialogueText.text = "";

            foreach (Transform child in _choicesBoxTransform)
            {
                Destroy(child.gameObject);
            }
            
            _choicesBoxTransform.gameObject.SetActive(false);
        }

        public void Visit()
        {
            // _listenToInput = true;
            // _nextNode = node.GetNextNode(null);
            _dialogueNodeChannel.RaiseRequestDialogueNode(null);
        }

        // public void Visit(ChoiceDialogueNode node)
        // {
        //     _choicesBoxTransform.gameObject.SetActive(true);
        //
        //     foreach (DialogueChoice choice in node.NextNodes)
        //     {
        //         DialogueChoiceController newChoice = Instantiate(_choiceControllerPrefab, _choicesBoxTransform);
        //         newChoice.Choice = choice;
        //     }
        // }
    }
}