using System;
using UnityEngine;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        private void Start()
        {
            m_FireAbility.UpdateText();
            m_TimeAbility.UpdateText();
        }
        private void Update()
        {
            if (TDPlayer.Instanse.m_Gold < m_FireAbility.m_cost)
            {
                Instanse.m_FireButton.interactable = false;
            }
            if (TDPlayer.Instanse.m_Gold < m_TimeAbility.m_cost)
            {
                Instanse.m_TimeButton.interactable = false;
            }
        }

        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_Cost;
            public int m_cost => m_Cost;
            [SerializeField] private float m_Cooldown = 5f;
            [SerializeField] private int m_Damage = 100;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private Text m_CostText;

            public void UpdateText()
            {
                m_CostText.text = m_Cost.ToString();
            }

            public void Use()
            {
                TDPlayer.Instanse.ChangeGold(-m_Cost);
                ClickProtection.Instanse.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
                });
            }

        }

        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int m_Cost;
            public int m_cost => m_Cost;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5;
            [SerializeField] private Text m_CostText;


            public void UpdateText()
            {
                m_CostText.text = m_Cost.ToString();
            }

            public void Use()
            {
                TDPlayer.Instanse.ChangeGold(-m_Cost);
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfMaxLinearVelocity();
                }

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);

                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreMaxLinearVelocity();
                    }

                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                Instanse.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instanse.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instanse.m_TimeButton.interactable = true;
                }

                Instanse.StartCoroutine(TimeAbilityButton());
            }

        }

        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button m_TimeButton;
        [SerializeField] private Button m_FireButton;
        [SerializeField] private FireAbility m_FireAbility;

        public void UseFireAbilitu() =>m_FireAbility.Use();
        [SerializeField] private TimeAbility m_TimeAbility;

        public void UseTimeAbilitu() => m_TimeAbility.Use();
    }
}

