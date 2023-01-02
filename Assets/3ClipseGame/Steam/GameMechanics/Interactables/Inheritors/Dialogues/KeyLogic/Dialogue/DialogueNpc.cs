using System;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.UI;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Dialogue
{
    public class DialogueNpc: Interactable<DialoguePresenter>
    {
        public override event Action<Interactable<DialoguePresenter>> Disappeared;

        public override DialoguePresenter GetPresenter()
        {
            throw new NotImplementedException();
        }

        public override void Activate()
        {
        }
    }
}