using _3ClipseGame.Steam.GameMechanics.Interactables.Display;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic
{
    public class DialogueEventsHandler: MonoBehaviour
    {
        [SerializeField] private InteractablesInputProcessor _inputProcessor;
        [SerializeField] private DialogueUIController _dialogueUIController;

        private void OnEnable() => _inputProcessor.Interacted += OnInteracted;
        
        private void OnDisable() => _inputProcessor.Interacted -= OnInteracted;

        private void OnInteracted()
        {
            _dialogueUIController.Visit();
        }
    }
}