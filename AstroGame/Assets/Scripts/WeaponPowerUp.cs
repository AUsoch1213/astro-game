using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class WeaponPowerUp : PowerUp
    {

        [SerializeField] private TurretProperties m_Properties;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssigneWeapon(m_Properties);
        }
    }
}

