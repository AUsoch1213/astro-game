using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image m_joystick;
        [SerializeField] private Image m_joyBack;

        public Vector3 Value { get; private set; }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_joyBack.rectTransform, eventData.position, eventData.pressEventCamera, out position);

            position.x = (position.x / m_joyBack.rectTransform.sizeDelta.x);
            position.y = (position.y / m_joyBack.rectTransform.sizeDelta.y);

            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            Value = new Vector3(position.x, position.y, 0);

            if (Value.magnitude > 1)
            {
                Value = Value.normalized;
            }

            float offsetX = m_joyBack.rectTransform.sizeDelta.x / 2 - m_joystick.rectTransform.sizeDelta.x / 2;
            float offsetY = m_joyBack.rectTransform.sizeDelta.y / 2 - m_joystick.rectTransform.sizeDelta.y / 2;

            m_joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, offsetY * Value.y);

            Debug.Log(Value);
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;

            m_joystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}

