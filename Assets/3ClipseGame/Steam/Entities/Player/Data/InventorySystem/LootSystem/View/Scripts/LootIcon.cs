using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.View.Scripts
{
    public class LootIcon : MonoBehaviour
    {
        #region PublicFields

        public Resource Resource { get; set; }
        public int Amount { get; set; }

        #endregion

        #region SerializeFields

        [SerializeField] private GameObject _activateOnActive;
        [SerializeField] private Image _resourceImage;
        [SerializeField] private Text _resourceText;

        #endregion

        #region MonoBehaviourMethods

        private void Update()
        {
            _resourceImage.sprite = Resource.UIImage;
            _resourceText.text = "x" + Amount;
        }

        #endregion

        #region PublicMethods

        public void SetActive(bool isActive)
        {
            _activateOnActive.SetActive(isActive);
        }

        #endregion
    }
}
