using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Camera m_Camera;

        [SerializeField] Transform m_Transform;

        [SerializeField] float m_InterpolationLinear;
        [SerializeField] float m_InterpolationAngular;

        [SerializeField] float m_CameraZOffset;
        [SerializeField] float m_ForwardOffset;

        private void FixedUpdate()
        {
            if (m_Transform == null || m_Camera == null) return;

            Vector2 camPos = Camera.main.transform.position;
            Vector2 tarPos = m_Transform.position + m_Transform.transform.up * m_ForwardOffset;
            Vector2 newCamPos = Vector2.Lerp(camPos, tarPos, m_InterpolationLinear * Time.deltaTime);

            m_Camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset);

            if (m_InterpolationAngular > 0)
            {
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Transform.rotation, m_InterpolationAngular * Time.fixedDeltaTime);
            }
        }
        public void SetTarget(Transform m_NewTarget)
        {
            m_Transform = m_NewTarget;
        }

    }
}
