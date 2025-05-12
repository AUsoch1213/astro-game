using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image m_PimaryTurret;
    [SerializeField] Image m_SecondaryTurret;
    private bool m_IsHold;
    public bool IsHold => m_IsHold;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
    }
}
