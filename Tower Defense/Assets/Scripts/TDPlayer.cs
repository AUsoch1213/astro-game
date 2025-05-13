using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instanse 
        { get 
            { 
                return Player.Instanse as TDPlayer;
            }
        }

        [SerializeField] private UpgradeAsset healhtUpgrade;

        private void Start()
        {
            OnGoldUpdate(m_gold);
            OnLifeUpdate(m_gold);
            OnManaUpdate(NumLives);

            var level = Upgrades.GetUpgradeLevel(healhtUpgrade);
            TakeDamage(-level * 5);
        }

        public event Action<int> OnGoldUpdate;

        public void GoldUpdateSubscride(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instanse.m_gold);
        }

        public event Action<int> OnManaUpdate;

        public void ManaUpdateSubscride(Action<int> act)
        {
            OnManaUpdate += act;
            act(Instanse.m_mana);
        }

        public event Action<int> OnLifeUpdate;

        public void LifeUpdateSubscride(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instanse.NumLives);
        }

        [SerializeField] private int m_gold = 0;
        public int m_Gold => m_gold;
        
        [SerializeField] private int m_mana = 0;
        
        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }

        public void ChangeMana(int change)
        {
            m_mana += change;
            OnManaUpdate(m_mana);
        }

        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }

        [SerializeField] private GameObject m_towerPrefab;

        public void TryBuild(TowerAsset towerAsset, Transform buildSize)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildSize.position, Quaternion.identity);
            tower.GetComponent<Tower>().Use(towerAsset);
            Destroy(buildSize.gameObject);
        }
    }
}
