using TMPro;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.UI
{
    public class DialoguePresenter: InteractablePresenter
    {
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _hint;

        public void SetNpcName(string newNpcName) => _characterName.text = newNpcName;
    }
}