using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SircleArea : MonoBehaviour
{
    [SerializeField] private float m_Radius;
    public float Radius => m_Radius;

    public Vector2 GetRandomInsideZone()
    {
        return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius;
    }

    private static Color GizmosColor = new Color(0, 1, 0, 0.3f);
    #if UNITY_EDITOR
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmosColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
    }
#endif
}
