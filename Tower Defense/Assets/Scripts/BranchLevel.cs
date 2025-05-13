using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel m_rootLevel;
        [SerializeField] private Text m_pointText;
        [SerializeField] private int m_needPoints = 3;

        /// <summary>
        /// Попытка активации определённого уровня.
        /// Активация требует наличия очков и прохождения прошлого уровня.
        /// </summary>

        public void TryActivate()
        {
            
            gameObject.SetActive(m_rootLevel.IsComplete);

            if (m_needPoints > MapCompletion.Instanse.TotalScore)
            {
                m_pointText.text = m_needPoints.ToString();
            }
            else
            {
                m_pointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }
    }
}