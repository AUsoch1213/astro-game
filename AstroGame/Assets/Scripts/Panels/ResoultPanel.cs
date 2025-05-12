using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_NextButtonText;

        private bool m_LevelPassed = false;
        private void Start()
        {
            gameObject.SetActive(false);
            LevelController.Inctance.LevelLost += OnLevelLost;
            LevelController.Inctance.LevelPassed += OnLevelPassed;
        }

        private void OnDestroy()
        {
            LevelController.Inctance.LevelLost -= OnLevelLost;
            LevelController.Inctance.LevelPassed -= OnLevelPassed;
        }


        private void OnLevelLost()
        {
            gameObject.SetActive(true);
            FillLevelStatistics();
            m_Result.text = "Lose";
            m_NextButtonText.text = "Restart";
        }

        private void OnLevelPassed()
        {
            gameObject.SetActive(true);
            m_LevelPassed = true;
            FillLevelStatistics();
            m_Result.text = "Passed";
            if (LevelController.Inctance.HasNextLevel == true)
            {
                m_NextButtonText.text = "Next";
            }
            else
            {
                m_NextButtonText.text = "MainMenu";
            }
        }

        private void FillLevelStatistics()
        {
            m_Kills.text = "Kills : " + Player.Inctance.Kills.ToString();
            m_Score.text = "Scores : " + Player.Inctance.Score.ToString();
            m_Time.text = "Time : " + LevelController.Inctance.LevelTime.ToString("F0");

        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive (false);

            if (m_LevelPassed == true)
            {
                LevelController.Inctance.LoadNextLevel();
            }
            else
            {
                LevelController.Inctance.RestartLevel();
            }
        }
    }
}

