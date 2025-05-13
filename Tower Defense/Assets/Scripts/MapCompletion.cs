using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string filename = "completion.dat";

        [Serializable]

        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        [SerializeField] private EpisodeScore[] completionData;
        public int TotalScore { private set; get; }

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);

            foreach (var episodeScore in completionData)
            {
                TotalScore += episodeScore.score;
            }
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instanse)
            {
                foreach (var item in Instanse.completionData)
                {//Сохранение новых очков прохождения
                    if (item.episode == LevelSequenceController.Instanse.CurrentEpisode)
                    {
                        if (levelScore > item.score)
                        {
                            Instanse.TotalScore += levelScore - item.score;
                            item.score = levelScore;
                            Saver<EpisodeScore[]>.Save(filename, Instanse.completionData);
                        }
                    }
                }
            }
            
        }

        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in completionData)
            {
                if (data.episode == m_episode)
                {
                    return data.score;
                }
            }
            return 0;
        }
    }

}