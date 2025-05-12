using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_Properties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip SpaceShip;

        #region Start || Update
        private void Start()
        {
            SpaceShip = transform.root.GetComponent<SpaceShip>();
        }
        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
            
        }
        #endregion

        #region Public API
        public void Fire()
        {
            if (m_Properties == null) return;

            if (CanFire == false) return;

            if (SpaceShip.DrawEnergy(m_Properties.EnergyUsage) == false)
                return;
            if (SpaceShip.DrawAmmo(m_Properties.AmmoUsage) == false)
                return;

            Projectile projectile = Instantiate(m_Properties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            m_RefireTimer = m_Properties.RateOfFire;


        }
        public void AssignetLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;
            m_RefireTimer = 0;
            m_Properties = props;
        }

        #endregion
    }
}

