using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelController : SingletoneBase<LevelController>
    {
        public event UnityAction LevelPassed;
        public event UnityAction LevelLost;

        [SerializeField] private LevelProperties m_LevelProperties;
        [SerializeField] private LevelCondition[] m_LevelConditions;
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        private bool m_IsLevelComplited;

        public bool HasNextLevel => m_LevelProperties.NextLevel != null;


        private void Start()
        {
            Time.timeScale = 1;
            m_LevelTime = 0;
        }

        private void Update()
        {
            if (m_IsLevelComplited == false)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }
        }
        private void CheckLevelConditions()
        {
            int numComplited = 0;
            for (int i = 0; i < m_LevelConditions.Length; i++)
            {
                

                if (m_LevelConditions[i].IsComplited == true)
                {
                    numComplited++;
                    Pass();
                }

                if (numComplited == m_LevelConditions.Length)
                {
                    m_IsLevelComplited = true;
                    
                }

            }
        }
        public void Loss()
        {
            LevelLost?.Invoke();
            Time.timeScale = 0;
        }
        private void Pass()
        {
            LevelPassed?.Invoke();
            Time.timeScale = 0;
        }
        public void LoadNextLevel()
        {
            if (HasNextLevel == true)
            {
                SceneManager.LoadScene(m_LevelProperties.NextLevel.SceneName);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(m_LevelProperties.SceneName);
        }
    }
}

