using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletoneBase<Player>
    {
        public static SpaceShip SelectSpaceShipPref;

        [SerializeField] private int m_NumLives;
        public int NumLives => m_NumLives;
        
        [SerializeField] private SpaceShip m_PrefShip;
        public SpaceShip ActiveShip => m_Ship;

        [SerializeField] private CameraController m_Camera;
        [SerializeField] private MovementController m_MovementCont;

        private SpaceShip m_Ship;
        private int m_Score;
        private int m_Kills;

        public int Score => m_Score;
        public int Kills => m_Kills;

        public SpaceShip SpaceShipPref
        {
            get
            {
                if (SelectSpaceShipPref == null)
                {
                    return m_PrefShip;
                }
                else
                {
                    return SelectSpaceShipPref;
                }
            }
        }
        private void Start()
        {
            Respawn();
        }
        private void Update()
        {
            if (ActiveShip == null)
            {
                OnShopDeath();
            }
            
        }
        [SerializeField] private LevelController m_LevelController;
        
        private void OnShopDeath()
        {
            m_NumLives--;
            

            if (m_NumLives > 0)
            {
                Respawn();
            }
            if (m_NumLives < 0)
            {
                m_NumLives = 0;
                m_LevelController.Loss();
            }
            
        }
        private void Respawn()
        {
            var newPlayerShip = Instantiate(SpaceShipPref);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();

            m_MovementCont.SetTargetShip(m_Ship);
            m_Camera.SetTarget(m_Ship.transform);

        }
        public void NumKills()
        {
            m_Kills += 1;
        }
        public void NumScores(int num)
        {
            m_Score += num;
        }

    }
}

