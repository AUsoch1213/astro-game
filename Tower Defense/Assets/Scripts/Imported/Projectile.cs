using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class Projectile : ProjectileBase
    {

        /*        protected override void OnHit(Destructible destructible)
                {
                    if (m_Parent == Player.Instanse.ActiveShip)
                    {
                        Player.Instanse.AddScore(destructible.ScoreValue);

                        if (destructible is SpaceShip)
                        {
                            if(destructible.HitPoints <= 0)
                                Player.Instanse.AddKill();
                        }
                    }
                }
        */

        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            if (m_ImpactEffectPrefab != null)
                Instantiate(m_ImpactEffectPrefab, pos, Quaternion.identity);

            Destroy(gameObject, 0);
        }
    }
}

