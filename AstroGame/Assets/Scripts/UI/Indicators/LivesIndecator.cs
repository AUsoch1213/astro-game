using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceShooter
{
    public class LivesIndecator : MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private float lastLives;
        private void Update()
        {
            
            int lives = Player.Inctance.NumLives;
            if (lives != lastLives)
            {
                m_Text.text = lives.ToString();
                lastLives = lives;
            }
        }
    }
}
