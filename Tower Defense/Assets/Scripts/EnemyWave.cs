﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.VersionControl;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave: MonoBehaviour
    {
        public static Action<float> OnWavePrepare;

        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField] private PathGroup[] groups;

        [SerializeField] private float prepareTime = 10f;

        public float GetRemainingTime() { return prepareTime - Time.time; }

        private void Awake()
        {
            enabled = false;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, 1);
                }
            }
        }

        private event Action OnWaveReady;

        internal void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(prepareTime);
            prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        private void Update()
        {
            if (Time.time >= prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        [SerializeField] private EnemyWave next;

        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (next) next.Prepare(spawnEnemies);
            return next;
        }
    }
}