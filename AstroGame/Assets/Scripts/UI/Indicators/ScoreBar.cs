using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceShooter
{
    public class ScoreBar: MonoBehaviour
    {
        [SerializeField] private Text m_Text;

        private float lastNumScores;
        private void Update()
        {
            
            int numScores = Player.Inctance.Score;
            if (numScores != lastNumScores)
            {
                m_Text.text = "Score : " + numScores.ToString();
                lastNumScores = numScores;
            }
        }
    }
}
