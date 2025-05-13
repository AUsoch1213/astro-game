using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// Панель результатов уровня. Должна лежать в каждом уровне без галочки DoNotDestroyOnLoad.
    /// </summary>
    public class LevelResultController : SingletonBase<LevelResultController>
    {
        [SerializeField] private GameObject m_PanelSuccess;
        [SerializeField] private GameObject m_PanelFailure;

        [SerializeField] private Text m_LevelTime;
        [SerializeField] private Text m_TotalPlayTime;
        [SerializeField] private Text m_TotalScore;
        [SerializeField] private Text m_TotalKills;

        /// <summary>
        /// Показываем окошко результатов. Выставляем нужные кнопочки в зависимости от успеха.
        /// </summary>
        /// <param name="result"></param>
        public void Show(bool result)
        {
            /*
            if (result)
            {
                UpdateCurrentLevelStats();
                UpdateVisualStats();
            }
            */

            m_PanelSuccess?.gameObject.SetActive(result);
            m_PanelFailure?.gameObject.SetActive(!result);
        }

        /// <summary>
        /// Запускаем следующий уровен. Дергается эвентом с кнопки play next.
        /// </summary>
        public void OnPlayNext()
        {
            LevelSequenceController.Instanse.AdvanceLevel();
        }

        /// <summary>
        /// Рестарт уровня. Дергается эвентом с кнопки restart в случае фейла уровня.
        /// </summary>
        public void OnRestartLevel()
        {
            LevelSequenceController.Instanse.RestartLevel();
        }


        public class Stats
        {
            public int numKills;
            public float time;
            public int score;
        }

        /// <summary>
        /// Общая статистика за эпизод.
        /// </summary>
        public static Stats TotalStats { get; private set; }

        /// <summary>
        /// Сброс общей статистики за эпизод. Вызывается перед началом эпизода.
        /// </summary>
        public static void ResetPlayerStats()
        {
            TotalStats = new Stats();
        }

        /// <summary>
        /// Собирает статистику по текущему уровню.
        /// </summary>
        /// <returns></returns>
        private void UpdateCurrentLevelStats()
        {
            TotalStats.numKills += Player.Instanse.NumKills;
            TotalStats.time += LevelController.Instanse.LevelTime;
            TotalStats.score += Player.Instanse.Score;

            // бонус за время прохождения.
            int timeBonus = (int) (LevelController.Instanse.ReferenceTime - LevelController.Instanse.LevelTime);

            if(timeBonus > 0)
                TotalStats.score += timeBonus;
        }

        /// <summary>
        /// Метод обновления статов уровня.
        /// </summary>
        private void UpdateVisualStats()
        {
            m_LevelTime.text = System.Convert.ToInt32(LevelController.Instanse.LevelTime).ToString();
            m_TotalScore.text = TotalStats.score.ToString();
            m_TotalPlayTime.text = System.Convert.ToInt32(TotalStats.time).ToString();
            m_TotalKills.text = TotalStats.numKills.ToString();
        }
    }
}