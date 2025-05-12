using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private int m_Damage;
        
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_LifeTime;
        [SerializeField] private ImpactEffect m_ImpactEffect;

        private float m_Timer;

        private void Update()
        {
            float steplength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * steplength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, steplength);

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();
                
                if (dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damage);
                    if (dest != Player.Inctance.ActiveShip)
                    {
                        Player.Inctance.NumScores(dest.ScoreValue);
                        if (dest is SpaceShip)
                        {
                            if (dest.CurrentHitPoints <= 0)
                            {
                                Player.Inctance.NumKills();
                            }
                        }
                    }
                }

                OnProjectileEnd(hit.collider, hit.point);
            }



            if (m_Timer < m_LifeTime)
                m_LifeTime += Time.deltaTime;

            else
                Destroy(gameObject);
            

            transform.position += new Vector3(step.x, step.y, 0);
        }
        private void OnProjectileEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }
        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
            
        }

    }
}

