using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float m_TimerLife;

    private float m_Timer;

    private void Update()
    {
        if (m_Timer < m_TimerLife)
        {
            m_TimerLife += Time.deltaTime;

        }
        else
        {
            Destroy(gameObject);
        }

    }


}
