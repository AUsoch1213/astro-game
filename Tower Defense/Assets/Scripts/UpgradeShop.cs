using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {

        [SerializeField] private int money;
        [SerializeField] private Text moneyText;
        [SerializeField] private BuyUpgrade[] sales;

        private void Start()
        {
            UpdateMoney();
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }
        }

        public void UpdateMoney()
        {
            //money = MapCompletion.Instanse.TotalScore;
            money -= Upgrades.GetTotalCost();
            moneyText.text = money.ToString();

            foreach (var slot in sales)
            {
                slot.CheckCost(money);
            }
        }
    }
}