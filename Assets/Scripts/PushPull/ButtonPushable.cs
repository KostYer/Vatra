using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player
{
    public class ButtonPushable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button _button;
        public event Action OnButtonPressed;
        public event Action OnButtonUnpressed;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnButtonPressed?.Invoke();
        }
          
          
        
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
          {
              OnButtonUnpressed?.Invoke();
          }

        public void Show(bool show)
        {
            _button.GetComponent<Image>().enabled = show;
            _button.interactable = show;
            
            _button.transform.GetChild(0).gameObject.SetActive(show);
        }
    }
}