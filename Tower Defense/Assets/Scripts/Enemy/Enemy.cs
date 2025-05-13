using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using Common;

namespace TowerDefense
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType { Base = 0, Mage = 1 }

        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int power, TDProjectile.DamageType type, int armor) =>
            {//Armor.Type.Base
                switch (type)
                {
                    case TDProjectile.DamageType.Magic: return power; 
                    default: return Math.Max(power - armor, 1);
                }
            },

            (int power, TDProjectile.DamageType type, int armor) =>
            {//Armor.Type.Magic
                if (TDProjectile.DamageType.Base == type)
                {
                    armor = armor / 2;
                }

                return Math.Max(power - armor, 1);
            }
        };

        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_gold = 1;
        [SerializeField] private int m_mana = 1;
        [SerializeField] private int m_armor = 1;
        [SerializeField] private ArmorType m_armorType;

        private Destructible m_destructible;

        private void Awake()
        {
            m_destructible = GetComponent<Destructible>();
        }

        public event Action OnEnd;

        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_damage = asset.damage;
            m_armor = asset.armor;
            m_armorType = asset.armorType;
            m_gold = asset.gold;
            m_mana = asset.mana;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instanse.ReduceLife(m_damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instanse.ChangeGold(m_gold);
        }

        public void GivePlayerMana()
        {
            TDPlayer.Instanse.ChangeMana(m_mana);
        }

        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {
            m_destructible.ApplyDamage(ArmorDamageFunctions[(int)m_armorType](damage, damageType, m_armor));
        }
    }

    [CustomEditor(typeof(Enemy))]

    public class EnemyInspector : Editor
    {
        public override void  OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }
}

