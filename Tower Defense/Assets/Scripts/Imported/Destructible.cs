using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TowerDefense;

namespace Common
{
    /// <summary>
    /// ������������ ������ �� �����. �� ��� ����� ����� ��� ������.
    /// </summary>
    public class Destructible : Entity
    {


        #region Properties
        /// <summary>
        /// ������ ���������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestractible;
        public bool IsIndestractible => m_Indestractible;

        /// <summary>
        /// ��������� ���-�� ����������.
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;

        /// <summary>
        /// ������� ��� ������.
        /// </summary>
        [SerializeField] private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        #endregion

        #region Uniti Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;

            transform.SetParent(null);
        }

        #endregion

        #region Public API

        /// <summary>
        /// ���������� ������ � �������.
        /// </summary>
        /// <param name="damage"> ���� ��������� ������� </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestractible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
                OnDeath();
            
        }

        #endregion

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� ��� ������ ���� ����.
        /// </summary>

        protected virtual void OnDeath()
        {
            m_EventOnDeath?.Invoke();

            Destroy(gameObject);
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        protected void Use(EnemyAsset asset)
        {
            m_HitPoints = asset.hp;
            m_ScoreValue = asset.score;
        }
    }
    

}
