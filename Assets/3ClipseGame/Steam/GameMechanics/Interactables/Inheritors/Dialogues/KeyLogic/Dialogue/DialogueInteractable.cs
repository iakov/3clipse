using System;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueInteractable : Interactable
    {
        [SerializeField] private DialoguePresenter _dialoguePresenter;
        [SerializeField] private string _characterName;
        
        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter()
        {
            var newPresenter = Instantiate(_dialoguePresenter);
            newPresenter.ChangeInteractable(this);
            _dialoguePresenter.SetNpcName(_characterName);
            return newPresenter;
        }

        public override void Activate()
        {
            //Say DialogueInstigator to start dialogue
        }
    }
}