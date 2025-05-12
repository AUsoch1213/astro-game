using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_LevelSelect;
    [SerializeField] private GameObject m_ShipSelect;
    [SerializeField] private GameObject m_MainPanel;


    public void ShowMainPanel()
    {
        m_MainPanel.gameObject.SetActive(true);
        m_ShipSelect.gameObject.SetActive(false);
        m_LevelSelect.gameObject.SetActive(false);
    }

    public void ShowShipSelectionPanel()
    {
        m_ShipSelect.gameObject.SetActive(true);
        m_MainPanel.gameObject.SetActive(false);
    }
    public void ShowLevelSelectionPanel()
    {
        m_LevelSelect.gameObject.SetActive(true);
        m_MainPanel.gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
