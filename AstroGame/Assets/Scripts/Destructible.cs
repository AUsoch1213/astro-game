
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Может ли повреждаться объект
        /// </summary>
        [SerializeField] private bool m_InDestructible;
        public bool InDestructible => m_InDestructible;
        /// <summary>
        /// Кол-во хит поинтов
        /// </summary>
        [SerializeField] private int m_hitPoints;
        public int HitPoints => m_hitPoints;

        /// <summary>
        /// Текущее кол-во хит поинтов
        /// </summary>
        private int m_currentHitPoints;
        public int CurrentHitPoints => m_currentHitPoints;
        #endregion


        #region UnityEvent
        protected virtual void Start()
        {
            m_currentHitPoints = m_hitPoints;
        }
        #endregion



        #region Public API
        public void ApplyDamage(int damage)
        {
            if (m_InDestructible) return;

            m_currentHitPoints -= damage;
            if (m_currentHitPoints <= 0)
            {
                OnDeath();
            }

        }
        #endregion
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventDeath?.Invoke();
        }


        private static HashSet<Destructible> m_AllDestructible;
        public static IReadOnlyCollection<Destructible> AllDestructible => m_AllDestructible;
        protected virtual void OnEnable()
        {
            if (m_AllDestructible == null)
            {
                m_AllDestructible = new HashSet<Destructible>();
                
            }
            m_AllDestructible.Add(this);
        }
        protected virtual void OnDestroy()
        {
            m_AllDestructible.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventDeath;
        public UnityEvent EventDeath => m_EventDeath;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

    }
}
