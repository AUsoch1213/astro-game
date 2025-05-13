using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;
using System;
using UnityEngine.SocialPlatforms.Impl;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;

        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] resultImages;

        public bool IsComplete { get { return gameObject.activeSelf && resultPanel.gameObject.activeSelf; } }

        public void LoadLevel()
        {
            LevelSequenceController.Instanse.StartEpisode(m_episode);
        }

        public int Initialise()
        {
            var score = MapCompletion.Instanse.GetEpisodeScore(m_episode);

            resultPanel.gameObject.SetActive(score > 0);

            for (int i = 0; i < score; i++)
            {
                resultImages[i].color = Color.white;
            }
            return score;
        }
    }
}

