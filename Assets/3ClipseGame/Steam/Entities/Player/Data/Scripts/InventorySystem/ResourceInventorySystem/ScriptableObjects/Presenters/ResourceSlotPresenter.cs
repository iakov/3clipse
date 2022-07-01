using System;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects.Resources.Presenters
{ 
    public class ResourceSlotPresenter : MonoBehaviour
    {
        #region PublicFields

        [NonSerialized] public ResourceSlot ResourceSlot;

        #endregion

        #region PrivateFields

        [SerializeField] private Image currentImage;
        [SerializeField] private Text amountText;

        #endregion

        #region MonoBehaviourMethods

        private void Start() => UpdateView();

        #endregion

        #region PublicMethods

        public void UpdateView()
        {
            if (ResourceSlot == null || ResourceSlot.Resource == null)
            {
                currentImage.sprite = null;
                amountText.text = "";
                return;
            }
            
            currentImage.sprite = ResourceSlot.Resource.UIImage;
            amountText.text = ResourceSlot.CurrentAmount.ToString();
        }

        #endregion
    }
}
