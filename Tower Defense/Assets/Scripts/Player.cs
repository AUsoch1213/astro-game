using UnityEngine;
using System;


namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        public static SpaceShip SelectedSpaceShip;

        [SerializeField] private int m_NumLives;
        public int NumLives { get { return m_NumLives; } }

        public event Action OnPlayerDead;

        [SerializeField] private SpaceShip m_PlayerShipPrefab;
        public SpaceShip ActiveShip => m_Ship;
        public static object Instance { get; internal set; }


        private Transform m_SpawnPoint;


        private SpaceShip m_Ship;

        private int m_Score;
        private int m_NumKills;

        public int Score => m_Score;
        public int NumKills => m_NumKills;
        

        public SpaceShip ShipPrefab
        {
            get
            {
                if (SelectedSpaceShip == null)
                {
                    return m_PlayerShipPrefab;
                }
                else
                {
                    return SelectedSpaceShip;
                }
            }
        }

        private void Start()
        {
            if (m_Ship)
            {
                Respawn();
            }
            
        }

        private void OnShipDeath()
        {
            m_NumLives--;
            Debug.Log("-1 lives OnShipDeath");

            if (m_NumLives > 0)
                Respawn();
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(ShipPrefab, m_SpawnPoint.position, m_SpawnPoint.rotation);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();

            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }

        public void AddKill()
        {
            m_NumKills += 1;
        }

        public void AddScore(int num)
        {
            m_Score += num;
        }

        protected void TakeDamage(int m_damage)
        {
            m_NumLives -= m_damage;

            if (m_NumLives <= 0)
            {
                m_NumLives = 0;
                OnPlayerDead?.Invoke();
            }
        }
    }
}

