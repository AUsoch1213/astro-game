﻿using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset: ScriptableObject
    {
        public int goldCost = 15;
        public Sprite sprite;
        public Sprite GUISprite;
        public TurretProperties turretProperties;
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private int requiredUpgradeLevel;

        public bool IsAvailable() => !requiredUpgrade || requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);

        public TowerAsset[] m_UpgradesTo;
       
    }

}