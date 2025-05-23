using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_Force;
        [SerializeField] private float m_Radius;

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.attachedRigidbody == null) return;

            Vector2 dir = transform.position - other.transform.position;

            float dist = dir.magnitude;

            if (dist < m_Radius)
            {
                Vector2 force = dir.normalized * m_Force * (dist / m_Radius);
                other.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
    }

}

