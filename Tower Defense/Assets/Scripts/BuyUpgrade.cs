using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        private int costNumber = 0;
        [SerializeField] private Text level, costText;
        [SerializeField] private Button buyBotton;

        public void Initialize()
        {
            upgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);

            if (savedLevel >= asset.costByLevel.Length)
            {
                level.text = $"lvl: {savedLevel} (Max)";
                level.text += "(Max)";
                buyBotton.interactable = false;
                buyBotton.transform.Find("Image (1)").gameObject.SetActive(false);
                buyBotton.transform.Find("Text").gameObject.SetActive(false);
                costText.text = "X";
                costNumber = int.MaxValue;
            }
            else
            {
                level.text = $"lvl: {savedLevel + 1}";
                costNumber = asset.costByLevel[savedLevel];
                costText.text = costNumber.ToString();
            }
        }

        public void CheckCost(int money)
        {
            buyBotton.interactable = money >= costNumber;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }
    }
}