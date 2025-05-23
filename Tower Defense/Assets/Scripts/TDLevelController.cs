using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;

        private new void Start()
        {
            base.Start();
            TDPlayer.Instanse.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instanse.Show(false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();

                if (m_ReferenceTime <= Time.time)
                {
                    levelScore -= 1;
                }

                MapCompletion.SaveEpisodeResult(levelScore);
            });

            void LifeScoreChange(int _)
            {
                levelScore -= 1;
                TDPlayer.Instanse.OnLifeUpdate -= LifeScoreChange;
            }

            TDPlayer.Instanse.OnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
        }
    }
}

