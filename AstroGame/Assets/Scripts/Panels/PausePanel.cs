using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject m_Panel;
    private void Start()
    {
        m_Panel.SetActive(false);
        Time.timeScale = 1;
    }
    public void ShowPause()
    {
        m_Panel.SetActive(true);
        Time.timeScale = 0;
    }
    public void HidePause()
    {
        m_Panel.SetActive(false);
        Time.timeScale = 1;
    }
    public void LoadMainMenu()
    {
        m_Panel.SetActive(false);
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }

}
