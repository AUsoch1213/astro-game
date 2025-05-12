using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public enum SpawnMode
    {
        Start,
        Loop
    }
    [SerializeField] private Entity[] m_PrefabEntity;
    [SerializeField] private SircleArea m_Area;
    [SerializeField] private SpawnMode m_SpawnMode;
    [SerializeField] private int m_NumberSpawns;
    [SerializeField] private float m_RespawnTimer;

    private float m_Timer;

    private void Start()
    {
        if (m_SpawnMode == SpawnMode.Start)
        {
            SpawnEntities();
        }
        m_Timer = m_RespawnTimer;
    }

    private void Update()
    {
        if (m_Timer > 0)
        {
            m_Timer -= Time.deltaTime;
        }
        if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
        {
            SpawnEntities();
            m_Timer = m_RespawnTimer;
        }
    }

    private void SpawnEntities()
    {
        for (int i = 0; i < m_NumberSpawns; i++)
        {
            int index = Random.Range(0, m_PrefabEntity.Length);
            GameObject E = Instantiate(m_PrefabEntity[index].gameObject);

            E.transform.position = m_Area.GetRandomInsideZone();
        }
    }

}
