using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0f, 4f)]
        [SerializeField] float m_ParallaxEffect;

        [SerializeField] float m_TextureScale;

        private Material m_Material;
        private Vector2 m_InitialOffset;


        private void Start()
        {
            m_Material = GetComponent<MeshRenderer>().material;
            m_InitialOffset = UnityEngine.Random.insideUnitCircle;

            m_Material.mainTextureScale = Vector2.one * m_TextureScale;

        }

        private void Update()
        {
            Vector2 offset = m_InitialOffset;

            offset.x += transform.position.x / transform.localScale.x / m_ParallaxEffect;
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxEffect;

            m_Material.mainTextureOffset = offset;
        }

    }
}
