using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectionPanel : MonoBehaviour
{
    [SerializeField] LevelProperties m_Properties;
    [SerializeField] TextMeshProUGUI m_LevelName;
    [SerializeField] Image m_LevelImage;

    private void Start()
    {
        if (m_Properties == null) return;

        m_LevelImage.sprite = m_Properties.Image;
        m_LevelName.text = m_Properties.SceneName;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(m_Properties.SceneName);
    }
}
