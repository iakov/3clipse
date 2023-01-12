using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue.DialogueNode.Choice;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic
{
    public class DialogueChoiceController: MonoBehaviour
    {
        [SerializeField] private TMP_Text _choice;
        [SerializeField] private DialogueNodeChannel _dialogueNodeChannel;
        
        private Choice _transitionChoice;
        private DialogueNode _choiceNextNode;
        
        public DialogueChoice DialogueChoice
        {
            set
            {
                _choice.text = value.TransitionChoice.Speech.SpeechText;
                _transitionChoice = value.TransitionChoice;
                _choiceNextNode = value.ChoiceNode;
            }
        }
        
        private void Start() => GetComponent<Button>().onClick.AddListener(OnClick);

        private void OnClick()
        {
            _dialogueNodeChannel.RaiseGetDialogueChoice(_transitionChoice);
            if (_choiceNextNode != null)
                _dialogueNodeChannel.RaiseRequestDialogueNode(_choiceNextNode);
            else
                _dialogueNodeChannel.RaiseEndDialogue(null);
        }
    }
}