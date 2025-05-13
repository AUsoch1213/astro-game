using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSourse { Gold, Life, Mana }
        public UpdateSourse sourse = UpdateSourse.Gold;

        private Text m_text;

        private void Start()
        {
            m_text = GetComponent<Text>();
            switch (sourse)
            {
                case UpdateSourse.Gold: TDPlayer.Instanse.OnGoldUpdate += UpdateText;
                    break;

                case UpdateSourse.Mana: TDPlayer.Instanse.OnManaUpdate += UpdateText;
                    break;

                case UpdateSourse.Life: TDPlayer.Instanse.OnLifeUpdate += UpdateText;
                    break;
            }
            
        }

        private void UpdateText(int text)
        {
            if (m_text != null)
            {
                m_text.text = text.ToString();
            }
            
        }
    }
}