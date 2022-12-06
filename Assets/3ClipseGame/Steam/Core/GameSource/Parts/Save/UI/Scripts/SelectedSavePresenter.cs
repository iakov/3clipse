using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class SelectedSavePresenter : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Vector2 _portalImageRatio;
        [SerializeField] private Image _imageComponent;

        private SavePresenter _currentPresenter;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_currentPresenter == null) return;
            _currentPresenter.Use();
        }
        
        public void ChangeImage(Sprite image, SavePresenter presenter)
        {
            var newImage = ConvertSpriteToPortalRatio(image);
            _imageComponent.sprite = newImage;
            _currentPresenter = presenter;
        }

        private Sprite ConvertSpriteToPortalRatio(Sprite primalSprite)
        {
            var horizontalSizeMultiplier = primalSprite.rect.width / _portalImageRatio.x;
            var verticalSizeMultiplier = primalSprite.rect.height / _portalImageRatio.y;

            var finalSizeMultiplier = Mathf.Min(horizontalSizeMultiplier, verticalSizeMultiplier);
            
            var finalWidth = _portalImageRatio.x *  finalSizeMultiplier;
            var finalHeight = _portalImageRatio.y * finalSizeMultiplier;

            var imageRect = new Rect(0, 0, finalWidth, finalHeight);
            var newSprite = Sprite.Create(primalSprite.texture, imageRect, Vector2.one / 2);
            return newSprite;
        }
    }
}
