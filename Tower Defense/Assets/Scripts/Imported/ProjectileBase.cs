using TowerDefense;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using Common;

namespace Common
{
    public abstract class ProjectileBase : Entity
    {
        public void SetFromOtherProjectile(ProjectileBase other)
        {
            other.GetData(out m_Velocity, out m_Lifetime, out m_Damage, out m_ImpactEffectPrefab);
        }

        private void GetData(out float m_Velocity, out float m_Lifetime, out int m_Damage, out ImpactEffect m_ImpactEffectPrefab)
        {
            m_Velocity = this.m_Velocity; 
            m_Lifetime = this.m_Lifetime; 
            m_Damage = this.m_Damage; 
            m_ImpactEffectPrefab = this.m_ImpactEffectPrefab;
        }

        [SerializeField] private float m_Velocity;

        [SerializeField] private float m_Lifetime;

        [SerializeField] protected int m_Damage;

        [SerializeField] protected ImpactEffect m_ImpactEffectPrefab;

        protected virtual void OnHit(Destructible destructible) { } 
        protected virtual void OnHit(Collider2D collider2D) { } 
        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos) { } 

        private float m_Timer;
        protected Destructible m_Parent;

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                OnHit(hit);

                OnProjectileLifeEnd(hit.collider, hit.point);
            }


            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                OnProjectileLifeEnd(hit.collider, hit.point);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        protected virtual void OnHit(RaycastHit2D hit)
        {
            OnHit(hit.collider);

            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != m_Parent)
            {
                dest.ApplyDamage(m_Damage);

                OnHit(dest);
            }
        }



        public void SetParrentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}

namespace TowerDefense
{
    [CustomEditor(typeof(ProjectileBase))]
    public class ProjectileInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create TD Projectile"))
            {
                var target = this.target as ProjectileBase;
                var tdProj = target.AddComponent<TDProjectile>();
                tdProj.SetFromOtherProjectile(target);
            }
        }
    }
}

