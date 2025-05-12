using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelProperties : ScriptableObject
{
    [SerializeField] private string m_Title;
    [SerializeField] private string m_SceneName;
    [SerializeField] private Sprite m_Image;
    [SerializeField] private LevelProperties m_NextLevel;

    public string Title => m_Title;
    public string SceneName => m_SceneName;
    public Sprite Image => m_Image;
    public LevelProperties NextLevel => m_NextLevel;
}
