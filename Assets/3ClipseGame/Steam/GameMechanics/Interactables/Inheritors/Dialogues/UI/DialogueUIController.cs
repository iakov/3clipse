using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue;
using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.UI
{
    public class DialogueUIController: MonoBehaviour
    {
        [SerializeField] private TMP_Text _speakerText;
        [SerializeField] private TMP_Text _dialogueText;

        [SerializeField] private RectTransform _choicesBoxTransform;
        [SerializeField] private DialogueEventsHandler _choiceControllerPrefab;

        [SerializeField] private Dialogue _dialogue;

        private bool _listenToInput = false;

        private void Awake()
        {
            gameObject.SetActive(false);
            _choicesBoxTransform.gameObject.SetActive(false);
        }
    }
}