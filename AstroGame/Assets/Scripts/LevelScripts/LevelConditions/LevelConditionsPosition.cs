using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionsPosition : LevelCondition
    {
        [SerializeField] private float m_Radius;
        public override bool IsComplited
        {
            get
            {
                if (Player.Inctance.ActiveShip == null)
                {
                    return false;
                }
                if (Vector3.Distance(Player.Inctance.transform.position, transform.position) <= m_Radius)
                {
                    return true;
                }
                return false;
            }
        }
    }

}

