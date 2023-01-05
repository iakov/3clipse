using System;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueInteractable : Interactable
    {
        [SerializeField] private DialoguePresenter _dialoguePresenter;
        [SerializeField] private NarrationCharacter _narrationCharacter;
        [SerializeField] private Dialogue _dialogue;

        private string _characterName;
        private DialogueInstigator _dialogueInstigator;

        private void Awake()
        {
            _characterName = _narrationCharacter.CharacterName;
            _dialogueInstigator = new DialogueInstigator();
        }

        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter()
        {
            _dialoguePresenter.SetNpcName(_characterName);
            // var newPresenter = Instantiate(_dialoguePresenter);
            var newPresenter = _dialoguePresenter;
            return newPresenter;
        }

        public override void Activate() => _dialogueInstigator.TryStartDialogue(_dialogue);
    }
}