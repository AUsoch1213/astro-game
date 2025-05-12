using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : LevelCondition
    {
        [SerializeField] private int m_Score;
        public override bool IsComplited
        {
            get
            {
                if (Player.Inctance.ActiveShip == null)
                {
                    return false;
                }

                if (Player.Inctance.Score >= m_Score)
                {
                    return true;
                }

                return false;
            }
        }
    }
}

