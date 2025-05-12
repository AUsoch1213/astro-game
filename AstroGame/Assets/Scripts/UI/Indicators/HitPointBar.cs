using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace SpaceShooter
{
    public class HitPointBar : MonoBehaviour
    {
        [SerializeField] private Image m_Image;

        private float lastHitPoints;
        private void Update()
        {

            float hitPoints = ((float)Player.Inctance.ActiveShip.CurrentHitPoints / (float)Player.Inctance.ActiveShip.HitPoints);
            if (hitPoints != lastHitPoints)
            {
                m_Image.fillAmount = hitPoints;
                lastHitPoints = hitPoints;
            }
        }
    }
}
