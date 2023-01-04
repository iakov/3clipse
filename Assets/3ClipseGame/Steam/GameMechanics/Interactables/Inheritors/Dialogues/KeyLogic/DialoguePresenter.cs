using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic
{
    public class DialoguePresenter: InteractablePresenter
    {
        [SerializeField] private TMP_Text _hint;

        public void SetNpcName(string newNpcName) => _hint.text = "Press 'Enter' to talk with " +  newNpcName;
    }
}