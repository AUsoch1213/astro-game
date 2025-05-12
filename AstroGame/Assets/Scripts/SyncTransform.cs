using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransform : MonoBehaviour
{
    [SerializeField] Transform m_Target;
    [SerializeField] Transform m_Background;

    private void FixedUpdate()
    {
        m_Background.transform.position = new Vector3(m_Target.position.x, m_Target.position.y, m_Background.localPosition.z);
        
    }
    public void SetTarget(Transform m_NewTarget)
    {
        m_Background.transform.position = m_NewTarget.position;
    }

}
