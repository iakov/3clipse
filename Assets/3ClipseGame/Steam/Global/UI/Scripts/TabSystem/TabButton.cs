using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Global.UI.Scripts.TabSystem
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        #region Serialization

        [NonSerialized] public Image BackgroundImage;
        [SerializeField] private TabGroup tabGroup;
        [SerializeField] private GameObject tabArea;
        
        [Header("Images")]
        public Sprite tabIdle;
        public Sprite tabHover;
        public Sprite tabActive;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => tabGroup.Subscribe(this);

        private void OnEnable()
        {
            BackgroundImage = GetComponent<Image>();
            if (tabGroup == null) throw new Exception("Tab Group not serialized");
        }

        #endregion

        #region PointerMethods

        public void OnPointerEnter(PointerEventData eventData) => tabGroup.OnTabEnter(this);

        public void OnPointerClick(PointerEventData eventData) => tabGroup.OnTabClicked(this);

        public void OnPointerExit(PointerEventData eventData) => tabGroup.OnTabExit();
        

        #endregion

        #region PublicMethods

        public void SetTabActive(bool isActive) => tabArea.SetActive(isActive);

        #endregion
    }
}
