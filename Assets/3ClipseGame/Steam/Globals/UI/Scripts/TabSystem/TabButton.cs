using System;
using _3ClipseGame.Steam.Globals.UI.Scripts.TabSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.UI.Scripts.TabSystem
{
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public Image backgroundImage;
        [SerializeField] private TabGroup tabGroup;
        [SerializeField] private GameObject tabArea;

        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
            if (tabGroup == null) throw new Exception("Tab Group not serialized");
            tabGroup.Subscribe(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabClicked(this);
            tabArea.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit();
        }

        public void SetTabActive(bool isActive) => tabArea.SetActive(isActive);
    }
}
