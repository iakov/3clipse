using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts.Display
{
    public class LootDisplay : MonoBehaviour
    {
        #region PublicFields

        public Resource Resource { get; set; }
        public int Amount { get; set; }

        #endregion

        #region SerializeFields

        [SerializeField] private GameObject activateOnActive;
        [SerializeField] private Image resourceImage;
        [SerializeField] private Text resourceText;

        #endregion

        #region MonoBehaviourMethods

        private void Update()
        {
            resourceImage.sprite = Resource.UIImage;
            resourceText.text = "x" + Amount;
        }

        #endregion

        #region PublicMethods

        public void SetActive(bool isActive) => activateOnActive.SetActive(isActive);

        #endregion
    }
}
