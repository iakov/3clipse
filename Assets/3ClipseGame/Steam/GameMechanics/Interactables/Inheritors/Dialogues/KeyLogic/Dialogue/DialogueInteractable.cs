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
        [SerializeField] private DialogueInstigator _dialogueInstigator;
        
        private string _characterName;

        private void Awake()
        {
            _characterName = _narrationCharacter.CharacterName;
        }

        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter()
        {
            _dialoguePresenter.SetNpcName(_characterName);
            return _dialoguePresenter;
        }

        public override void Activate() => _dialogueInstigator.TryStartDialogue(_dialogue);
    }
}