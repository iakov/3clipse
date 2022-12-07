using _3ClipseGame.Steam.Mechanics.Save.InGame;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters
{
    public class EmptySavePresenter : SavePresenter
    {
        [SerializeField] private Sprite _newSaveSprite;
        
        public override void Use()
        {
            SaveManager.Instance.NewGame();
        }

        protected override Sprite GetImage()
        {
            return _newSaveSprite;
        }
    }
}