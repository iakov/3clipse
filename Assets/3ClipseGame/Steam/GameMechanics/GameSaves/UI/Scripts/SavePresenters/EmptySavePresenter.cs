using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.SavePresenters
{
    public class EmptySavePresenter : SavePresenter
    {
        [SerializeField] private Sprite _newSaveSprite;
        
        public override void Use() => InterSceneSavesEntry.NewGame();

        protected override Sprite GetImage() => _newSaveSprite;
    }
}