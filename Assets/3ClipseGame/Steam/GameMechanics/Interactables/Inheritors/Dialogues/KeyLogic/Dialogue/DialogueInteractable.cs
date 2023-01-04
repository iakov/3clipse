using System;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueInteractable : Interactable
    {
        [SerializeField] private DialoguePresenter _dialoguePresenter;
        [SerializeField] private NarrationCharacter _narrationCharacter;

        private string _characterName;
        private DialogueInstigator _dialogueInstigator;

        private void Awake() => _characterName = _narrationCharacter.CharacterName;

        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter()
        {
            _dialoguePresenter.SetNpcName(_characterName);
            // var newPresenter = Instantiate(_dialoguePresenter);
            var newPresenter = _dialoguePresenter;
            newPresenter.ChangeInteractable(this);
            return newPresenter;
        }

        public override void Activate()
        {
            // _dialogueInstigator.TryStartDialogue();
            //Say DialogueInstigator to start dialogue
        }
    }
}