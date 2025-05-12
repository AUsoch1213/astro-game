using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


namespace SpaceShooter
{
    public class KillBar: MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private float lastNumKills;
        private void Update()
        {
            
            int numKills = Player.Inctance.Kills;
            if (numKills != lastNumKills)
            {
                m_Text.text = "Kills : " + numKills.ToString();
                lastNumKills = numKills;
            }
        }
    }
}
