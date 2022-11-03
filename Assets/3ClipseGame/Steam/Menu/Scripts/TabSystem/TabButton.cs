using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Menu.Scripts.TabSystem
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        private Image _backgroundImage;
        [SerializeField] private TabGroup tabGroup;
        [SerializeField] private GameObject tabArea;
        
        [Header("Images")]
        [SerializeField] private Sprite tabIdle;
        [SerializeField] private Sprite tabHover;
        [SerializeField] private Sprite tabActive;

        private void Awake()
        {
            _backgroundImage = GetComponent<Image>();
            if (tabGroup == null) throw new Exception("Tab Group not serialized");
        }

        public void OnPointerEnter(PointerEventData eventData) => tabGroup.OnTabEnter(this);

        public void OnPointerClick(PointerEventData eventData) => tabGroup.OnTabClicked(this);

        public void OnPointerExit(PointerEventData eventData) => tabGroup.OnTabExit();
        

        public void SetTabActive(bool isActive)
        {
            if (_backgroundImage == null) _backgroundImage = GetComponent<Image>();
            
            if (isActive) _backgroundImage.sprite = tabActive;
            else _backgroundImage.sprite = tabIdle;
            
            tabArea.SetActive(isActive);
        }

        public void SetTabScoped() => _backgroundImage.sprite = tabHover;
    }
}
