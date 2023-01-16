using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic
{
    public class DialoguePresenter : InteractablePresenter
    {
        [SerializeField] private TMP_Text _hint;

        public override void Activate() => CurrentInteractable.Activate();
        
        public void SetNpcName(string newNpcName) => _hint.text = "Talk with " + newNpcName;
    }
}