using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipSelectionPanel : MonoBehaviour
{
    [SerializeField] private SpaceShip m_Pref;
    [SerializeField] private MainMenu m_MainMenu;
    [SerializeField] private TextMeshProUGUI m_ShipName;
    [SerializeField] private TextMeshProUGUI m_Hitpoints;
    [SerializeField] private TextMeshProUGUI m_Speed;
    [SerializeField] private TextMeshProUGUI m_Agility;
    [SerializeField] private Image m_Preview;

    private void Start()
    {
        if (m_Pref == null)
        {
            return;
        }
        m_ShipName.text = m_Pref.Name;
        m_Hitpoints.text = "HP : " + m_Pref.HitPoints.ToString();
        m_Speed.text = "Speed : " + m_Pref.MaxLinearVelocity.ToString();
        m_Agility.text = "Agility : " + m_Pref.MaxAngularVelocity.ToString();
        m_Preview.sprite = m_Pref.PreviewImage;
    }

    public void SelectShip()
    {
        Player.SelectSpaceShipPref = m_Pref;
        m_MainMenu.ShowMainPanel();
    }

}
