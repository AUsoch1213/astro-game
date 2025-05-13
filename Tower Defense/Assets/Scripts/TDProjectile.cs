using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using Common;

namespace TowerDefense
{
    public class TDProjectile : ProjectileBase
    {
        public enum DamageType { Base, Magic }
        [SerializeField] private DamageType m_DamageType;

        protected override void OnHit(RaycastHit2D hit)
        {
            OnHit(hit.collider);

            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage, m_DamageType);
            }
        }
    }
}